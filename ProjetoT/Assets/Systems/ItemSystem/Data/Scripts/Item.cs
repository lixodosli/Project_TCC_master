using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item : Interactable
{
    [SerializeField] private bool m_InteractWithClosest;
    [SerializeReference] public List<ItemEffect> Effects = new List<ItemEffect>();

    [ContextMenu("Add Consumable")] public void AddConsumable() => Effects.Add(new ConsumableEffect(this));
    [ContextMenu("Add Food")] public void AddFood() => Effects.Add(new HungryEffect(this));
    [ContextMenu("Add Transform To")] public void AddTransformTo() => Effects.Add(new TransformToEffect(this));

    private void Awake()
    {
        UpdateParent();
    }

    private void Start()
    {
        if(ItemID == null)
        {
            Debug.Log("O item <" + gameObject.name + "> Está sem ID, portanto precisa de um ID.");
        }

        foreach (ItemEffect effect in Effects)
        {
            effect.Setup(this);
        }
    }

    private void OnTransformParentChanged()
    {
        UpdateParent();
    }

    public void UseEffects()
    {
        foreach (ItemEffect effect in Effects)
        {
            effect.UseEffect();
        }
    }

    public void SetItemNameAndId(string name, string id)
    {
        m_ItemName = name;
        m_ItemID = id;
        m_ToolTip = GetComponent<ToolTip>();
    }

    public virtual void UseItem()
    {
        Useable_Set closest = ClosestUseable();

        if (m_InteractWithClosest)
        {
            if (closest == null)
            {
                FeedbackMessage.ShowFeedback("Não há uso para este item aqui.");
                return;
            }

            if (closest.TryUse(this))
            {
                if (OnInteractSFX != null)
                {
                    AudioManager.Instance.Play(OnInteractSFX, AudioType.SFX, AudioConfigs.Default());
                }

                //closest.UseUseable(this);
            }
        }
        else
        {
            UseEffects();
        }
    }

    public override void DoInteraction()
    {
        if (GameStateManager.Game.State != GameState.World_Free)
            return;

        Inventory.Instance.CollectItem(this);
    }

    protected virtual Useable_Set ClosestUseable()
    {
        return NearbyUseableSets.ClosestUseableSet;
    }

    public void UpdateParent()
    {
    }

    protected virtual void RestoreItem()
    {
    }
}

[System.Serializable]
public class ItemEffect
{
    protected Item _Item;

    public ItemEffect(Item item)
    {
        _Item = item;
    }

    public virtual void Setup(Item item)
    {
        _Item = item;
    }

    public virtual void UseEffect()
    {
        return;
    }
}

public class ConsumableEffect : ItemEffect
{
    public List<ItemList> DropOnConsume;

    public ConsumableEffect(Item item) : base(item)
    {
    }

    public override void UseEffect()
    {
        Inventory.Instance.ConsumeItem(_Item);

        for (int i = 0; i < DropOnConsume.Count; i++)
        {
            ItemSpawnPoint.InstItem(DropOnConsume[i], _Item.transform);
        }

        base.UseEffect();
    }
}

public class HungryEffect : ItemEffect
{
    public int HungryChange;

    public HungryEffect(Item item) : base(item)
    {
    }

    public override void UseEffect()
    {
        Messenger.Broadcast(HungrySystem.HungryName, HungryChange);

        base.UseEffect();
    }
}

public class TransformToEffect : ItemEffect
{
    public Item TransformToItem;

    public TransformToEffect(Item item) : base(item)
    {
    }

    public override void UseEffect()
    {
        _Item = TransformToItem;

        base.UseEffect();
    }
}

public class UseStackEffect : ItemEffect
{
    public int Stack;
    private int _StackCount;

    public UseStackEffect(Item item) : base(item)
    {
    }

    public override void Setup(Item item)
    {
        base.Setup(item);
        _StackCount = Stack;
    }

    public override void UseEffect()
    {
        _StackCount--;

        if(_StackCount <= 0)
        {
            Inventory.Instance.ConsumeItem(_Item);
        }

        base.UseEffect();
    }
}