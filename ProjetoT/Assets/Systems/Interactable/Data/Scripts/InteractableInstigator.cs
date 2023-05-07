using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableInstigator : MonoBehaviour
{
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
    }
}