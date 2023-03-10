using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Save Channel")]
public class SaveChannel : ScriptableObject
{
    public delegate void SaveCallback();
    public SaveCallback OnSave;
    public SaveCallback OnLoad;

    public void RaiseSave()
    {
        OnSave?.Invoke();
    }

    public void RaiseLoad()
    {
        OnLoad?.Invoke();
    }
}
