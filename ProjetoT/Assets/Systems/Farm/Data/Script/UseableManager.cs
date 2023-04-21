using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableManager : MonoBehaviour
{
    public static UseableManager Instance { get; private set; }

    public delegate void UseableCallback(UseableInfo info);
    public UseableCallback OnUseable;

    public delegate void UseableUseCallback(Useable use);
    public UseableUseCallback OnUseableUse;

    public delegate void UseableStateCallback(UseableObjInfo info);
    public UseableStateCallback OnRequestChangeState;

    private void Awake()
    {
        Instance = this;
    }

    public void RaiseUseable(UseableInfo info)
    {
        OnUseable?.Invoke(info);
    }

    public void RequestChangeState(UseableObjInfo obj)
    {
        OnRequestChangeState?.Invoke(obj);
    }
}

public struct UseableInfo
{
    private Item _Item;
    private Useable_Object _Obj;

    public Item Item => _Item;
    public Useable_Object Obj => _Obj;

    public UseableInfo(Item item, Useable_Object obj)
    {
        _Item = item;
        _Obj = obj;
    }
}

public struct UseableObjInfo
{
    private Useable _Usb;
    private Useable_Object _Obj;

    public Useable Usb => _Usb;
    public Useable_Object Obj => _Obj;

    public UseableObjInfo(Useable usb, Useable_Object obj)
    {
        _Usb = usb;
        _Obj = obj;
    }
}