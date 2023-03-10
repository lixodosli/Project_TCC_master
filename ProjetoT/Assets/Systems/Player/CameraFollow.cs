using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float m_FollowSpeed = 10f;
    [SerializeField] private Transform m_Camera;
    [SerializeField] private Transform m_CameraPivot;
    [SerializeField] private Transform m_Target;
    public Transform Camera => m_Camera;
    public Transform CameraPivot => m_CameraPivot;
    public Transform Target => m_Target;

    private float m_MaxYRotation;
    private float m_MouseXRotation;

    private bool _PauseMovement = true;

    private void Awake()
    {
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed += PauseMovement;
    }

    private void OnDestroy()
    {
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed -= PauseMovement;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (_PauseMovement)
        {
            m_MaxYRotation = m_MaxYRotation - ((Input.GetAxisRaw("Mouse X") * -1) * 2 * 100 * Time.deltaTime);
        }

            //m_Target.Rotate(0f, m_MouseXRotation, 0f, Space.World);
            m_CameraPivot.rotation = Quaternion.Lerp(m_CameraPivot.rotation, Quaternion.Euler(m_Target.eulerAngles.x, m_MaxYRotation * 2, 0f), 100 * Time.deltaTime);
            m_CameraPivot.position = Vector3.Lerp(m_CameraPivot.position, m_Target.position, m_FollowSpeed * Time.deltaTime);
    }

    private void PauseMovement(InputAction.CallbackContext context)
    {
        _PauseMovement = !_PauseMovement;

        //if (!_PauseMovement)
        //    Cursor.lockState = CursorLockMode.None;
        //else
        //    Cursor.lockState = CursorLockMode.Locked;
    }

    public void ChangeTarget(Transform newTarget)
    {
        m_Target = newTarget;
    }
}