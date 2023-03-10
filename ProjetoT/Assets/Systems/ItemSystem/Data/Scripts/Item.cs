using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemEvent : UnityEvent<Item>
{
}

public abstract class Item : ScriptableObject
{
    [Header("Basic Item Info")]
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemIcon;

    [Header("Item Effects")]
    public ItemEvent OnUse;
    public ItemEvent OnCancel;
    public ItemEvent OnSelect;
    public ItemEvent OnDiscard;
    public ItemEvent OnGet;

    public bool HaveOnUse = true;
    public bool HaveOnCancel;
    public bool HaveOnSelect;
    public bool HaveOnDiscard;
    public bool HaveOnGet;

    public abstract bool CanUse();

    public virtual void Use()
    {
        if (!CanUse())
            return;

        OnUse?.Invoke(this);
    }
}