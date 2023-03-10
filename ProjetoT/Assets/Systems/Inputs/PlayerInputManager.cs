using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;
    public PlayerInputActions PlayerInput { get; private set; }

    private void Awake()
    {
        Instance = this;

        PlayerInput = new PlayerInputActions();
        PlayerInput.Enable();
    }
}