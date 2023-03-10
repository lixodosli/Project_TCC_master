using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour
{
    private Renderer _Renderer;
    [SerializeField] private HighlightChannel m_Channel;
    private int HighlightMaterialIndex => _Renderer.materials.Length - 1;

    private void Awake()
    {
        _Renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        m_Channel.OnFindHighlightable += Highlight;
        m_Channel.OnLeaveHighlightable += StopHighlight;
    }

    private void OnDisable()
    {
        m_Channel.OnFindHighlightable -= Highlight;
        m_Channel.OnLeaveHighlightable -= StopHighlight;
    }

    private void Highlight(Highlightable highlitable)
    {
        if (highlitable == this)
        {
            _Renderer.materials[HighlightMaterialIndex].SetFloat("_HasOutline", 1);
        }
        else
        {
            _Renderer.materials[HighlightMaterialIndex].SetFloat("_HasOutline", 0);
        }
    }

    private void StopHighlight(Highlightable highlitable)
    {
        if (highlitable != this)
            return;

        _Renderer.materials[HighlightMaterialIndex].SetFloat("_HasOutline", 0);
    }
}