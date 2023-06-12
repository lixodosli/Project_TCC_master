using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects/Donations")]
public class DonationsStructure : ScriptableObject
{
    public List<int> DonationsMilestones = new List<int>();
    public int CurrentDonationsPoints = 0;
    public int CurrentMilestoneIndex;

    private void OnEnable()
    {
        CurrentDonationsPoints = 0;
    }

    private void OnValidate()
    {
        CurrentDonationsPoints = Mathf.Clamp(CurrentDonationsPoints, 0, int.MaxValue);
    }

    public void AddDonation(int add)
    {
        CurrentDonationsPoints += add;
        UpdateMilestone(CurrentDonationsPoints);
    }

    public void UpdateMilestone(int currentPoints)
    {
        if (CurrentMilestoneIndex >= DonationsMilestones.Count)
            return;

        if (currentPoints >= DonationsMilestones[CurrentMilestoneIndex + 1])
        {
            CurrentMilestoneIndex++;
            Debug.Log("AUMENTOU O LV");
        }
    }
}