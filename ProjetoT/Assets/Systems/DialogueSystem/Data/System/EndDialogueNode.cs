using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDialogueNode : DialogueNode
{
    [SerializeReference] public List<DialogueEffect> Effects = new List<DialogueEffect>();
}