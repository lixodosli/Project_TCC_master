using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] private int m_ChestSpace;
    public List<Item> ChestSlots = new List<Item>();

    public override void DoInteraction()
    {
        // Open the menu for chest interaction. The menu will be dynamic and 
    }
}