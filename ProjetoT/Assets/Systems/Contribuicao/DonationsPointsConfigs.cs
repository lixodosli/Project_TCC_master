using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Donations")]
public class DonationsPointsConfigs : ScriptableObject
{
    public List<PointsPair> PointsList = new List<PointsPair>();

    public int Points(Item item)
    {
        return PointsList.Find(a => a.Item.ItemName == item.ItemName).Points;
    }

    private void OnValidate()
    {
        PointsList.ForEach(a => a.Points = Mathf.Clamp(a.Points, 1, int.MaxValue));
    }
}

[System.Serializable]
public class PointsPair
{
    public Item Item;
    public int Points;
}