using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum TypeOfInteraction
{
    Anytime,
    Once,
    AnytimeWithDelay
}

public class Interactable : MonoBehaviour
{
    [SerializeField] private TypeOfInteraction m_InteractionType;
    [SerializeField] private float m_Delay;
    [SerializeField] UnityEvent m_OnInteraction;
    [HideInInspector] public bool AlreadyInteractWithThisObject = false;
    [HideInInspector] public bool CanInteract = true;
    [HideInInspector] public bool CanInteractBuffer = true;

    public void DoInteraction()
    {
        if (m_InteractionType == TypeOfInteraction.Once && AlreadyInteractWithThisObject || m_InteractionType == TypeOfInteraction.AnytimeWithDelay && !CanInteract)
            return;

        if (m_InteractionType == TypeOfInteraction.Once)
        {
            AlreadyInteractWithThisObject = true;
            CanInteract = false;
            CanInteractBuffer = false;
        }

        if(m_InteractionType == TypeOfInteraction.AnytimeWithDelay)
        {
            StartCoroutine(DoDelay());
        }
    }

    private IEnumerator DoDelay()
    {
        CanInteract = false;
        CanInteractBuffer = true;

        yield return new WaitForSeconds(m_Delay);

        CanInteract = true;
    }
}