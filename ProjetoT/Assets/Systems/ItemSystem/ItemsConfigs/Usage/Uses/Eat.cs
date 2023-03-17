using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : Usage
{
    public override void Use(Item item)
    {
        DisplayMessage();
    }
}