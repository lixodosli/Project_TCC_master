using UnityEngine;
using UnityEngine.InputSystem;

public class NewDialogueSystem : MonoBehaviour
{
    public static NewDialogueSystem Instance;

    [SerializeField] private DialogueSystem_UI m_DialogueUI;
    [SerializeField] private NewConversation m_Conversation;

    private DialogueNode _CurrentDialogueNode;

    private void Awake()
    {
        Instance = this;
    }

    public void StartConversation(NewConversation conversation)
    {
        m_Conversation = conversation;

        _CurrentDialogueNode = m_Conversation.FirstDialogue;
        if (_CurrentDialogueNode == null)
        {
            Debug.LogError("No first dialogue node found in the conversation.");
            return;
        }

        DisplayCurrentDialogue();
    }

    public void DisplayCurrentDialogue()
    {
        if (_CurrentDialogueNode != null)
        {
            m_DialogueUI.DisplayDialogue(_CurrentDialogueNode.Name, _CurrentDialogueNode.Text, _CurrentDialogueNode.LettersPerSecond);
        }
        else
        {
            EndConversation();
        }
    }

    public void EndConversation()
    {
        m_DialogueUI.HideDialogue();
        _CurrentDialogueNode = null;
        m_Conversation = null;
        GameStateManager.Game.RaiseChangeGameState(GameState.World_Free);
    }

    public void PassDialogue(InputAction.CallbackContext context)
    {
        if (m_DialogueUI.IsActive && GameStateManager.Game.State == GameState.Cutscene)
        {
            _CurrentDialogueNode.OnEndDialogue?.Invoke();
            _CurrentDialogueNode = _CurrentDialogueNode.Dialogue();
            DisplayCurrentDialogue();
        }
    }
}
