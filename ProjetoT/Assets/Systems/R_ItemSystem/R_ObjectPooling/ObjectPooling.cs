using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> objectPoolDictionary = new Dictionary<string, Queue<GameObject>>();

    public void CreatePool(GameObject prefab, int initialSize)
    {
        string poolKey = prefab.name;

        if (objectPoolDictionary.ContainsKey(poolKey))
        {
            Debug.LogWarning("Pool with key " + poolKey + " already exists.");
            return;
        }

        Queue<GameObject> objectPool = new Queue<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        objectPoolDictionary.Add(poolKey, objectPool);
    }

    public GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        string poolKey = prefab.name;

        if (!objectPoolDictionary.ContainsKey(poolKey))
        {
            Debug.LogWarning("Pool with key " + poolKey + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = objectPoolDictionary[poolKey].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        objectPoolDictionary[poolKey].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
