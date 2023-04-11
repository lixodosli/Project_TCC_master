using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Teleport/Teleport", fileName = "Teleport_")]
public class TeleportConfig : ScriptableObject
{
    private Transform _TeleportPosition;
    public Transform TeleportPosition => _TeleportPosition;

    public void SetupConfig(Transform spot)
    {
        _TeleportPosition = spot;
    }
}