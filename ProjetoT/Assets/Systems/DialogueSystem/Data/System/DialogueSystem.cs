using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    [SerializeField] private DialogueSystem_UI m_DialogueUI;
    [SerializeField] private List<Conversation> m_Conversations = new List<Conversation>();

    private Conversation _CurrentConversation;
    public NPC CurrentNPC;
    public Conversation CurrentConversation => _CurrentConversation;
    private int _CurrentDialogueIndex;

    private void Awake()
    {
        LoadConversations();
        Instance = this;
    }

    public void StartConversation(NPC npc)
    {
        //CurrentNPC = npc;

        //int narrativeIndex = npc.CurrentNarrativeIndex;
        //int conversationIndex = npc.Narratives[narrativeIndex].CurrentConversationIndex;
        //string conversationName = npc.Narratives[narrativeIndex].Conversations[conversationIndex].Name;

        //_CurrentConversation = m_Conversations.Find(conversation => conversation.Name == conversationName);

        //if (_CurrentConversation == null)
        //{
        //    Debug.LogError($"Conversation '{conversationName}' not found.");
        //    return;
        //}

        //if(_CurrentConversation.RequiredQuest != null)
        //{
        //    if (!PlayerQuests.Instance.IsCompleted(_CurrentConversation.RequiredQuest))
        //    {
        //        _CurrentConversation = _CurrentConversation.ConversationIfDontHaveRequiredQuest;
        //    }
        //}

        //_CurrentDialogueIndex = 0;
        //DisplayCurrentDialogue();

        //PlayerInputManager.Instance.PlayerInput.World.Action.performed += PassDialogue;
        //GameStateManager.Game.RaiseChangeGameState(GameState.Cutscene);
    }

    public void DisplayCurrentDialogue()
    {
        //if (_CurrentDialogueIndex < _CurrentConversation.Dialogues.Count)
        //{
        //    Dialogue dialogue = _CurrentConversation.Dialogues[_CurrentDialogueIndex];
        //    m_DialogueUI.DisplayDialogue(dialogue.Name, dialogue.Text, dialogue.Options, dialogue.LettersPerSecond);
        //}
        //else
        //{
        //    if(_CurrentConversation.StartQuest != null)
        //        PlayerQuests.Instance.AddQuest(_CurrentConversation.StartQuest);

        //    EndConversation();
        //}
    }

    public void EndConversation()
    {
        //m_DialogueUI.HideDialogue();
        //_CurrentConversation = null;
        //_CurrentDialogueIndex = 0;
        //CurrentNPC = null;
        //PlayerInputManager.Instance.PlayerInput.World.Action.performed -= PassDialogue;
        //GameStateManager.Game.RaiseChangeGameState(GameState.World_Free);
    }

    public void SelectOption(int optionIndex)
    {
        //if (_CurrentDialogueIndex < _CurrentConversation.Dialogues.Count)
        //{
        //    Dialogue dialogue = _CurrentConversation.Dialogues[_CurrentDialogueIndex];

        //    if (dialogue.Options != null && optionIndex >= 0 && optionIndex < dialogue.Options.Length)
        //    {
        //        _CurrentDialogueIndex = dialogue.Options[optionIndex];
        //        DisplayCurrentDialogue();
        //    }
        //}
    }

    public void LoadConversations()
    {
        //Conversation[] conversations = Resources.LoadAll<Conversation>("Conversations");
        //foreach (Conversation conversation in conversations)
        //{
        //    m_Conversations.Add(conversation);
        //}
    }

    public void PassDialogue(InputAction.CallbackContext context)
    {
        //if (m_DialogueUI.IsActive && GameStateManager.Game.State == GameState.Cutscene)
        //{
        //    _CurrentConversation.Dialogues[_CurrentDialogueIndex].OnEndDialogue?.Invoke();
        //    _CurrentDialogueIndex++;
        //    DisplayCurrentDialogue();
        //}
    }
}