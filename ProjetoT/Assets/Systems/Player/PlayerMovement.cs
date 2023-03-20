using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_RotationSpeed;
    [SerializeField] private Transform m_Orientation;
    [SerializeField] private Animator m_Animator;
    private Rigidbody _Rigidbody;
    private Vector2 _InputValue;
    private Vector3 _MoveDirection = new Vector3();
    private bool _CanMove = true;
    private bool _PauseMovement = true;

    private void Awake()
    {
        SetCanMove();
        _Rigidbody = GetComponent<Rigidbody>();
        R_Inventory.Instance.OnCollectItem += CollectItem;
        R_Inventory.Instance.OnOpenCloseInventory += PauseMovement;
    }

    private void OnDestroy()
    {
        R_Inventory.Instance.OnCollectItem += CollectItem;
        R_Inventory.Instance.OnOpenCloseInventory -= PauseMovement;
        //InventoryUI.Instance.OnCallInventory -= PauseMovement;
    }

    private void Update()
    {
        if (_CanMove && _PauseMovement)
        {
            _InputValue = PlayerInputManager.Instance.PlayerInput.World.Movement.ReadValue<Vector2>();
            _MoveDirection = m_Orientation.forward * _InputValue.y + m_Orientation.right * _InputValue.x;

            if(_MoveDirection.magnitude > 0.1f)
                transform.forward = Vector3.Slerp(transform.forward, _MoveDirection.normalized, Time.deltaTime * m_RotationSpeed);
        }

        m_Animator.SetFloat("Movement", _MoveDirection.magnitude);
    }

    private void SetCanMove()
    {
        _CanMove = true;
    }

    private void PauseMovement()
    {
        if (GameStateManager.Game.State == GameState.Inventory || GameStateManager.Game.State == GameState.Pause || GameStateManager.Game.State == GameState.Cutscene)
            _PauseMovement = false;
        else
            _PauseMovement = true;

        if (!_PauseMovement)
            _MoveDirection = Vector3.zero;
    }

    private void CollectItem()
    {
        _CanMove = false;
        m_Animator.SetTrigger("Pick");
        Invoke(nameof(SetCanMove), 0.5f);
    }

    private void FixedUpdate()
    {
        if(_CanMove && _PauseMovement)
            _Rigidbody.AddForce(_MoveDirection * m_Speed, ForceMode.Force);
    }
}