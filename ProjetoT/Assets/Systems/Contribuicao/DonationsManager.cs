using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonationsManager : MonoBehaviour
{
    public static DonationsManager Instance;
    public DonationsStructure Structure;
    public DonationsPointsConfigs PointsConfigs;

    public delegate void DonationsCallback(int total);
    public DonationsCallback OnAddDonation;

    private void Awake()
    {
        Instance = this;
    }

    public void AddDonation(int donation)
    {
        Structure.AddDonation(donation);
        OnAddDonation?.Invoke(Structure.CurrentDonationsPoints);
    }
}