using System;
using System.Collections;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;

    public delegate void TeleportCallback(TeleportConfig telep);
    public TeleportCallback OnRaiseTeleport;

    public GameObject Player;

    private void Awake()
    {
        Instance = this;
    }

    public void RaiseTeleport(TeleportConfig telep)
    {
        OnRaiseTeleport?.Invoke(telep);
        SceneTransition.Instance.StartFadeIn();
    }

    public void TeleportTo(Transform spot)
    {
        Player.transform.position = spot.position;
    }

    private IEnumerator UpdateDayCoroutine()
    {
        if (OnRaiseTeleport != null)
        {
            Delegate[] invocationList = OnRaiseTeleport.GetInvocationList();
            int eventCount = 0;

            while (eventCount < invocationList.Length)
            {
                Debug.Log("Tentando Invocar todos os eventos. Ainda há " + (invocationList.Length - eventCount) + " eventos na lista");
                yield return null;

                // Verifica quantas funções foram executadas
                eventCount = 0;
                foreach (Delegate d in invocationList)
                {
                    if (d != null)
                        eventCount++;
                }
            }

            Debug.Log("Todas as funcoes executadas");
            WaitForTeleport(1f);
        }
        else
        {
            Debug.Log("Nao existem funcoes a serem executadas");
            WaitForTeleport(1f);
        }
    }

    private void WaitForTeleport(float time)
    {
        Invoke(nameof(StartFadeOut), time);
    }

    private void StartFadeOut() => SceneTransition.Instance.StartFadeOut();
}