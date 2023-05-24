using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Useable_Set : MonoBehaviour
{
    public string SetName;
    private Useable[] _Useables;

    private Item _UsedItem;
    [SerializeField] private AudioClip m_OnChangeSFX;

    private void Awake()
    {
        SetupUseables();
        Messenger.AddListener<string>(SetName, UpdateSelectedUseable);
    }

    public void UpdateSelectedUseable(string tile)
    {
        int i = UseableByIndex(tile);

        Debug.Log($"The set {SetName} will turn of:\n{CurrentStateIndex()} - <{CurrentState().UseableName}>\npara\n{i} - <{_Useables[i].UseableName}>");

        SetActive(_Useables[i]);
    }

    public bool TryUse(Item item)
    {
        _UsedItem = item;

        if (CurrentState().CanBeUsed(_UsedItem))
        {
            _UsedItem = item;
            ExecutionBar.StartCounting(CurrentState().BarTime);
            ExecutionBar.Complete += UseItem;
            ExecutionBar.Canceled += CancelUse;
            return true;
        }

        return false;
    }

    public void UseUseable(Item item)
    {
        _UsedItem = item;
        ExecutionBar.StartCounting(CurrentState().BarTime);
        ExecutionBar.Complete += UseItem;
        ExecutionBar.Canceled += CancelUse;
    }

    private void UseItem()
    {
        CurrentState().Use(_UsedItem);
        ExecutionBar.Complete -= UseItem;
        ExecutionBar.Canceled -= CancelUse;
    }

    private void CancelUse()
    {
        ExecutionBar.Complete -= UseItem;
        ExecutionBar.Canceled -= CancelUse;
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

    public int CurrentStateIndex()
    {
        for (int i = 0; i < _Useables.Length; i++)
        {
            if (_Useables[i].gameObject.activeSelf)
            {
                return i;
            }
        }

        return -1;
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

        int newActive = 0;

        for (int i = 0; i < _Useables.Length; i++)
        {
            if (_Useables[i] != tile)
                _Useables[i].gameObject.SetActive(false);
            else
                newActive = i;
        }

        _Useables[newActive].gameObject.SetActive(true);
        AudioManager.Instance.Play(m_OnChangeSFX, AudioType.SFX, AudioConfigs.Default());
    }
}