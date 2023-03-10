using UnityEngine;
using SaveSystem;

public class InteractableSaveManager : MonoBehaviour, ISaveable
{
    [SerializeField] private Interactable[] m_Interactables;

    public object CaptureState()
    {
        bool[] alreadyInteract = new bool[m_Interactables.Length];
        bool[] canInteract = new bool[m_Interactables.Length];
        bool[] isActive = new bool[m_Interactables.Length];

        for (int i = 0; i < m_Interactables.Length; i++)
        {
            alreadyInteract[i] = m_Interactables[i].AlreadyInteractWithThisObject;
            canInteract[i] = m_Interactables[i].CanInteractBuffer;
            isActive[i] = m_Interactables[i].gameObject.activeSelf;
        }

        return new SaveData
        {
            AlreadyInteract = alreadyInteract,
            CanInteract = canInteract,
            IsActive = isActive
        };
    }

    public void RestoreState(object state)
    {
        var savedData = (SaveData)state;

        for (int i = 0; i < m_Interactables.Length; i++)
        {
            m_Interactables[i].AlreadyInteractWithThisObject = savedData.AlreadyInteract[i];
            m_Interactables[i].CanInteract = savedData.CanInteract[i];
            m_Interactables[i].CanInteractBuffer = savedData.CanInteract[i];
            m_Interactables[i].gameObject.SetActive(savedData.IsActive[i]);
        }
    }

    [System.Serializable]
    public struct SaveData
    {
        public bool[] AlreadyInteract;
        public bool[] CanInteract;
        public bool[] IsActive;
    }
}