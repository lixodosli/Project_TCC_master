using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Highlight Channel")]
public class HighlightChannel : ScriptableObject
{
    public delegate void HighlightCallback(Highlightable highlightable);
    public HighlightCallback OnFindHighlightable;
    public HighlightCallback OnLeaveHighlightable;

    public void RaiseHighlight(Highlightable highlightable)
    {
        OnFindHighlightable?.Invoke(highlightable);
    }

    public void RaiseLeaveHighlight(Highlightable highlightable)
    {
        OnLeaveHighlightable?.Invoke(highlightable);
    }
}