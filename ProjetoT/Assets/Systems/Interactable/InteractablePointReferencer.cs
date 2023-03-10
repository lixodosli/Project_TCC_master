using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePointReferencer : MonoBehaviour
{
    [SerializeField] private InteractionDisplayer m_Displayer;
    public InteractionDisplayer Displayer => m_Displayer;
}
