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
        PlayerInputManager.Instance.PlayerInput.World.PickItem.performed += PickItem;
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed += PauseMovement;
    }

    private void OnDestroy()
    {
        PlayerInputManager.Instance.PlayerInput.World.PickItem.performed -= PickItem;
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed -= PauseMovement;
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

    private void PauseMovement(InputAction.CallbackContext context)
    {
        _PauseMovement = !_PauseMovement;

        if (!_PauseMovement)
            _MoveDirection = Vector3.zero;
    }

    private void PickItem(InputAction.CallbackContext context)
    {
        if (Inventory.Instance.Interactables.HasNearbyInteractables() && Inventory.Instance.Interactables.ClosestInteractables().Settings.IsCollectable)
        {
            _CanMove = false;
            m_Animator.SetTrigger("Pick");
            Invoke(nameof(SetCanMove), 0.5f);
        }
    }

    private void FixedUpdate()
    {
        if(_CanMove && _PauseMovement)
            _Rigidbody.AddForce(_MoveDirection * m_Speed, ForceMode.Force);
    }
}