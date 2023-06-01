using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableInstigator : MonoBehaviour
{
    public static InteractableInstigator Instance { get; private set; }

    public delegate void InteractableCallback(Interactable interactable);
    public InteractableCallback OnInteract;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        PlayerInputManager.Instance.PlayerInput.World.Action.performed += OnInteractionTriggered;
    }

    private void OnDisable()
    {
        PlayerInputManager.Instance.PlayerInput.World.Action.performed -= OnInteractionTriggered;
    }
    
    private void OnInteractionTriggered(InputAction.CallbackContext context)
    {
        if (GameStateManager.Game.State != GameState.World_Free || NearbyInteractables.ClosestInteractable == null)
            return;

        NearbyInteractables.ClosestInteractable.DoInteraction();
        Debug.Log($"Trigou Interactable <{NearbyInteractables.ClosestInteractable.ItemName}>");
        OnInteract?.Invoke(NearbyInteractables.ClosestInteractable);
    }
}