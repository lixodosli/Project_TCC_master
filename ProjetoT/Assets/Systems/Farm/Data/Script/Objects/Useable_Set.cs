using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Useable_Set : MonoBehaviour
{
    public string SetName;
    private Useable[] _Useables;

    private void Awake()
    {
        SetupUseables();
        Messenger.AddListener<string>(SetName, UpdateSelectedUseable);
    }

    public void UpdateSelectedUseable(string tile)
    {
        SetActive(_Useables[UseableByIndex(tile)]);
    }

    public void UseUseable(Item item)
    {
        CurrentState().Use(item);
    }

    public Useable CurrentState()
    {
        for (int i = 0; i < _Useables.Length; i++)
        {
            if (_Useables[i].gameObject.activeSelf)
            {
                return _Useables[i];
            }
        }

        return null;
    }

    private void SetupUseables() => _Useables = GetComponentsInChildren<Useable>(true);

    private int UseableByIndex(string tile)
    {
        SetupUseables();

        for (int i = 0; i < _Useables.Length; i++)
        {
            if (_Useables[i].UseableName == tile)
                return i;
        }

        return -1;
    }

    private void SetActive(Useable tile)
    {
        SetupUseables();

        for (int i = 0; i < _Useables.Length; i++)
        {
            if (_Useables[i] == tile)
                _Useables[i].gameObject.SetActive(true);
            else
                _Useables[i].gameObject.SetActive(false);
        }
    }
}