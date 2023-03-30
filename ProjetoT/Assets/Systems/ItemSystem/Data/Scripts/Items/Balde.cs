using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balde : R_Item
{
    public override void UseItem()
    {
        Collider[] closeObjects = Physics.OverlapSphere(transform.position, 1.5f);
        List<Useable_Object> useableObjects = new List<Useable_Object>();

        bool haveUseableItem = false;

        for (int i = 0; i < closeObjects.Length; i++)
        {
            if (closeObjects[i].GetComponent<Useable_Object>() != null)
            {
                haveUseableItem = true;
                useableObjects.Add(closeObjects[i].GetComponent<Useable_Object>());
            }
        }

        if (!haveUseableItem)
            return;

        int closestIndex = 0;

        for (int i = 0; i < useableObjects.Count; i++)
        {
            if (Vector3.Distance(transform.position, useableObjects[i].transform.position) < Vector3.Distance(transform.position, useableObjects[closestIndex].transform.position))
            {
                closestIndex = i;
            }
        }

        UseableManager.Instance.RaiseUseable(new UseableInfo(this, useableObjects[closestIndex]));
    }
}