using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HighlightInstigator : MonoBehaviour
{
    private List<Highlightable> _NearbyHighlightable = new List<Highlightable>();
    [SerializeField] private HighlightChannel m_Channel;

    private void OnDisable()
    {
        _NearbyHighlightable.Clear();
    }

    private void Update()
    {
        if (HasNearbyHighlightable()) // Adicionar a condicao do mais proximo estar muito proximo do raycast do centro da tela
        {
            m_Channel.RaiseHighlight(ClosestHighlightable());
        }
    }

    public bool HasNearbyHighlightable() => _NearbyHighlightable.Count > 0;

    public Highlightable ClosestHighlightable()
    {
        int closestIndex = 0;

        for (int i = 0; i < _NearbyHighlightable.Count; i++)
        {
            var atualClosest = Vector3.Distance(_NearbyHighlightable[closestIndex].transform.position, transform.position);
            var newCheackage = Vector3.Distance(_NearbyHighlightable[i].transform.position, transform.position);

            if (newCheackage < atualClosest)
            {
                closestIndex = i;
            }
        }

        return _NearbyHighlightable[closestIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        Highlightable highlightable = other.GetComponent<Highlightable>();

        if (highlightable != null)
        {
            _NearbyHighlightable.Add(highlightable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Highlightable highlightable = other.GetComponent<Highlightable>();

        if (highlightable != null)
        {
            _NearbyHighlightable.Remove(highlightable);
            m_Channel.RaiseLeaveHighlight(highlightable);
        }
    }
}