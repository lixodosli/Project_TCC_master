using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Useable_Object : MonoBehaviour
{
    [SerializeField] private Useable[] m_State;
    public int StartState;
    private int _ActiveState;
    public int ActiveState => _ActiveState;

    private void Awake()
    {
        UseableManager.Instance.OnUseable += RaiseUse;
        UseableManager.Instance.OnRequestChangeState += ChangeStateByState;
    }

    private void OnDestroy()
    {
        UseableManager.Instance.OnUseable -= RaiseUse;
        UseableManager.Instance.OnRequestChangeState -= ChangeStateByState;
    }

    private void Start()
    {
        SetupStates();
        ChangeStateByIndex(StartState);
    }

    public void SetupStates()
    {
        for (int i = 0; i < m_State.Length; i++)
        {
            m_State[i].SetupObj(this);
        }
    }

    public void ChangeStateByIndex(int index)
    {
        for (int i = 0; i < m_State.Length; i++)
        {
            if(i == index)
            {
                m_State[i].gameObject.SetActive(true);
                _ActiveState = i;
            }
            else
            {
                m_State[i].gameObject.SetActive(false);
            }
        }
    }

    public void ChangeStateByState(UseableObjInfo info)
    {
        if (info.Obj != this)
            return;

        if (!CurrentState().CanBeFollowedByState(info.Usb))
            return;

        ChangeStateByIndex(IndexByState(info.Usb));
    }

    public void RaiseUse(UseableInfo info)
    {
        if (info.Obj != this)
            return;

        if (!CurrentState().CanBeUsed(info.Item))
            return;

        CurrentState().OnUsed(info.Item);
    }

    public Useable CurrentState() => m_State[ActiveState];

    public int IndexByState(Useable state)
    {
        for (int i = 0; i < m_State.Length; i++)
        {
            if (m_State[i] == state)
                return i;
        }

        return -1;
    }

    public Useable StateByIndex(int index) => m_State[index];
}