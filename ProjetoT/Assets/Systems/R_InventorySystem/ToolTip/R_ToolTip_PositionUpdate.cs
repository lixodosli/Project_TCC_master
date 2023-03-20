using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_ToolTip_PositionUpdate : MonoBehaviour
{
    public RectTransform Box;
    public Transform Target;
    public Vector3 TargetOffset;
    private bool _CanUpdate;

    private void Update()
    {
        if(_CanUpdate)
            UpdateTipBoxPosition();
    }

    public void SetupTipBoxPosition()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(Target.position + TargetOffset);

        Box.position = position;

        _CanUpdate = true;
    }

    private void UpdateTipBoxPosition()
    {
        if (!gameObject.activeSelf)
            return;

        Vector3 position = Camera.main.WorldToScreenPoint(Target.position + TargetOffset);

        Box.position = Vector3.Slerp(Box.position, position, Time.deltaTime * 50f);
    }
}