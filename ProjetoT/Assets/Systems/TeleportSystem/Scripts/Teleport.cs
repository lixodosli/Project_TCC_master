using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private TeleportConfig m_Config;
    public TeleportConfig Config => m_Config;

    private void Awake()
    {
        TeleportManager.Instance.OnRaiseTeleport += CallForTeleport;
    }

    private void OnDestroy()
    {
        TeleportManager.Instance.OnRaiseTeleport -= CallForTeleport;
    }

    private void Start()
    {
        m_Config.SetupConfig(transform);
    }

    public void CallForTeleport(TeleportConfig config)
    {
        if (config != m_Config)
            return;

        SceneTransition.Instance.OnSceneTransitionStart += DoTeleport;
    }

    private void DoTeleport()
    {
        TeleportManager.Instance.TeleportTo(m_Config.TeleportPosition);

        SceneTransition.Instance.OnSceneTransitionStart -= DoTeleport;
    }
}