using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Useable_Object : MonoBehaviour
{
    [SerializeField] private Useable[] m_State;
    private int _ActiveState;
    public int ActiveState => _ActiveState;

    private void Awake()
    {
        UseableManager.Instance.OnUseable += RaiseUse;
    }

    private void OnDestroy()
    {
        UseableManager.Instance.OnUseable -= RaiseUse;
    }

    public void RaiseUse(UseableInfo info)
    {
        if (info.Obj != this)
            return;

        if (!m_State[ActiveState].CanBeUsed(info.Item))
            return;

        m_State[ActiveState].OnUsed(info.Item);
    }
}