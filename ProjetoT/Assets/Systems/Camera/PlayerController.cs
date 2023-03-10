using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform m_Player;
    [SerializeField] private float m_Speed = 3f;
    [SerializeField] private float m_RunSpeed = 5f;
    private Vector3 m_MovementDirection;
    private Rigidbody m_PlayerRigidBody;
    private bool m_Running;

    [SerializeField] private float m_MaxYRotation;
    [SerializeField] private float m_MouseXRotation;

    [SerializeField] private Transform m_CameraPivot;
    [SerializeField] private Transform m_Camera;

    private void Awake()
    {
        m_PlayerRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        m_MovementDirection = m_Player.TransformVector(new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized);
        m_MaxYRotation = Mathf.Clamp(m_MaxYRotation - (Input.GetAxisRaw("Mouse Y") * 2 * 100 * Time.deltaTime), -30, 30);
        m_MouseXRotation = Mathf.Lerp(m_MouseXRotation, Input.GetAxisRaw("Mouse X") * 2, 100 * Time.deltaTime);
        m_Running = Input.GetButton("Crouch");

        m_Player.Rotate(0f, m_MouseXRotation, 0f, Space.World);
        m_Camera.rotation = Quaternion.Lerp(m_Camera.rotation, Quaternion.Euler(m_MaxYRotation * 2, m_Player.eulerAngles.y, 0f), 100 * Time.deltaTime);
        m_CameraPivot.position = Vector3.Lerp(m_CameraPivot.position, m_Player.position, 10 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        m_PlayerRigidBody.MovePosition(m_PlayerRigidBody.position + m_MovementDirection * (m_Running ? m_RunSpeed :  m_Speed) * Time.fixedDeltaTime);
    }
}

public struct MovementConfig
{

}