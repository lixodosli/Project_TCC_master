using UnityEngine;
using SaveSystem;

public class PlayerMovement : MonoBehaviour, ISaveable
{
    public SaveChannel Save;

    [SerializeField] private float m_Speed;
    [SerializeField] private float m_RotationSpeed;
    [SerializeField] private Transform m_Orientation;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private ParticleSystem m_MovementParticle;
    [SerializeField] private LayerMask m_TerrainLayer;
    [SerializeField] private LayerMask m_WoodenLayer;
    private Rigidbody _Rigidbody;
    private Vector2 _InputValue;
    private Vector3 _MoveDirection = new Vector3();
    private bool _CanMove = true;
    private bool _PauseMovement = true;

    private void Awake()
    {
        SetCanMove();
        _Rigidbody = GetComponent<Rigidbody>();
        R_Inventory.Instance.OnCollectItem += CollectItemAnim;
        R_Inventory.Instance.OnOpenCloseInventory += PauseMovement;
        DaySystem.Instance.OnDayEnd += FuncaoTeste;
    }

    private void OnDestroy()
    {
        R_Inventory.Instance.OnCollectItem += CollectItemAnim;
        R_Inventory.Instance.OnOpenCloseInventory -= PauseMovement;
        DaySystem.Instance.OnDayEnd -= FuncaoTeste;
    }

    private void Update()
    {
        if (_CanMove && _PauseMovement)
        {
            _InputValue = PlayerInputManager.Instance.PlayerInput.World.Movement.ReadValue<Vector2>();
            _MoveDirection = m_Orientation.forward * _InputValue.y + m_Orientation.right * _InputValue.x;

            if(_MoveDirection.magnitude > 0.1f)
            {
                transform.forward = Vector3.Slerp(transform.forward, _MoveDirection.normalized, Time.deltaTime * m_RotationSpeed);
            }
        }

        m_Animator.SetFloat("Movement", _MoveDirection.magnitude);

        if (Input.GetKeyDown(KeyCode.F12))
        {
            Save.RaiseSave();
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            Save.RaiseLoad();
        }
    }

    private void FuncaoTeste(int iha)
    {
        Debug.Log("Hoje e o dia " + iha);
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

    private void CollectItemAnim()
    {
        _CanMove = false;
        m_Animator.SetTrigger("Pick");
        Invoke(nameof(SetCanMove), 0.5f);
    }

    private void FixedUpdate()
    {
        if(_CanMove && _PauseMovement)
        {
            _Rigidbody.AddForce(_MoveDirection * m_Speed, ForceMode.Force);
            DoParticlePlay();
        }
    }

    private void DoParticlePlay()
    {
        if (GroundTypeLayerIndex() != 6)
        {
            m_MovementParticle.Stop();
            return;
        }

        if (_Rigidbody.velocity.magnitude > 0.2f && !m_MovementParticle.isPlaying)
        {
            m_MovementParticle.Play();
        }
        else if (_Rigidbody.velocity.magnitude <= 0.2f && m_MovementParticle.isPlaying)
        {
            m_MovementParticle.Stop();
        }
    }

    private int GroundTypeLayerIndex()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f))
            return hit.collider.gameObject.layer;
        else
            return 0;
    }

    [System.Serializable]
    private struct SaveData
    {
        public float xPos;
        public float yPos;
        public float zPos;

        public float xRot;
        public float yRot;
        public float zRot;

        public bool PauseMovement;
        public bool CanMove;
    }

    public object CaptureState()
    {
        return new SaveData
        {
            xPos = transform.position.x,
            yPos = transform.position.y,
            zPos = transform.position.z,

            xRot = transform.rotation.x,
            yRot = transform.rotation.y,
            zRot = transform.rotation.z,

            PauseMovement = _PauseMovement,
            CanMove = _CanMove
        };
    }

    public void RestoreState(object state)
    {
        var savedData = (SaveData)state;

        transform.SetPositionAndRotation(new Vector3(savedData.xPos, savedData.yPos, savedData.zPos), Quaternion.Euler(savedData.xRot, savedData.yRot, savedData.zRot));
        _CanMove = savedData.CanMove;
        _PauseMovement = savedData.PauseMovement;
    }
}