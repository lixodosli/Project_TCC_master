using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin_MediumGrowth : Useable
{
    [SerializeField] private GameObject m_Abroba;

    public override void OnUsed(R_Item item)
    {
        base.OnUsed(item);
        Vector3 position = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);

        Instantiate(m_Abroba, position, Quaternion.identity);
    }
}