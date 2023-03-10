using UnityEngine;

public class InteractableListenner : MonoBehaviour
{
    [SerializeField] private InteractionChannel m_Channel;
    [SerializeField] private GameObject m_MyFeedback;
    public bool active;
    private Interactable m_Interactable;

    private void Awake()
    {
        if (active)
        {
            m_Interactable = GetComponent<Interactable>();
            m_Channel.OnFindInteraction += ShowInterface;
            m_Channel.OnLeaveInteraction += HideInterface;
        }
    }

    private void OnDestroy()
    {
        if (active)
        {
            m_Channel.OnFindInteraction -= ShowInterface;
            m_Channel.OnLeaveInteraction -= HideInterface;
        }
    }

    private void ShowInterface(Interactable interactable)
    {
        if (active)
        {
            if (interactable != m_Interactable)
                return;

            m_MyFeedback.SetActive(true);
        }
    }

    private void HideInterface(Interactable interactable)
    {
        if (active)
        {
            if (interactable != m_Interactable)
                return;

            m_MyFeedback.SetActive(false);
        }
    }
}