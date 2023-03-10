using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Interactable Channel")]
public class InteractionChannel : ScriptableObject
{
    public delegate void InteractableCallback(Interactable interactable);
    public InteractableCallback OnFindInteraction;
    public InteractableCallback OnLeaveInteraction;

    public void RaiseInteractionAdd(Interactable interactable)
    {
        OnFindInteraction?.Invoke(interactable);
    }

    public void RaiseInteractionRemove(Interactable interactable)
    {
        OnLeaveInteraction?.Invoke(interactable);
    }
}