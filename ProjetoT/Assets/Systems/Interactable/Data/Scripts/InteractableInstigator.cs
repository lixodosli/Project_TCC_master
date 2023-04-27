using UnityEngine;

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

    private void OnInteractionTriggered(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (GameStateManager.Game.State != GameState.World_Free || NearbyInteractables.ClosestInteractable == null)
            return;

        NearbyInteractables.ClosestInteractable.DoInteraction();
    }
}