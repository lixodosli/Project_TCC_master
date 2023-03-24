using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableManager : MonoBehaviour
{
    public static UseableManager Instance { get; private set; }

    public delegate void UseableCallback(UseableInfo info);
    public UseableCallback OnUseable;

    private void Awake()
    {
        Instance = this;
    }

    public void RaiseUseable(UseableInfo info)
    {
        OnUseable?.Invoke(info);
    }
}

public struct UseableInfo
{
    private R_Item _Item;
    private Useable_Object _Obj;

    public R_Item Item => _Item;
    public Useable_Object Obj => _Obj;

    public UseableInfo(R_Item item, Useable_Object obj)
    {
        _Item = item;
        _Obj = obj;
    }
}