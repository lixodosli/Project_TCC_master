using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private ItemSpawnPoint[] _SpawnPoints;

    private void SetupSpawner()
    {
        ItemSpawnPoint[] points = FindObjectsOfType<ItemSpawnPoint>();

        _SpawnPoints = points;
    }
}