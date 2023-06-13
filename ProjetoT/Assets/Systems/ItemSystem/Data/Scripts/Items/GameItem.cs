using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : Item
{
    private void OnValidate()
    {
        if(ItemName != null)
            name = ItemName;
    }
}