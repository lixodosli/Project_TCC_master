using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;

public class ItemManager : MonoBehaviour, ISaveable
{
    public static ItemManager Instance;

    [Header("Items Data File")]
    [SerializeField] private TextAsset m_Data;

    private List<int> _ItemsInScene = new List<int>();
    public static Dictionary<int, string> ItemList = new Dictionary<int, string>();

    private void Awake()
    {
        Instance = this;
    }

    [ContextMenu("Setup Data")]
    public void SetupData()
    {
        ItemList = SheetsReader.ReadJSONData(m_Data.text);
    }

    [System.Serializable]
    private struct SaveData
    {
        public float[] XPos;
        public float[] YPos;
        public float[] ZPos;

        public float[] XRot;
        public float[] YRot;
        public float[] ZRot;

        public List<int> ItemsID;
    }

    public object CaptureState()
    {
        List<Item> sceneItems = Resources.FindObjectsOfTypeAll<Item>().ToList();

        float[] xPosSave = new float[sceneItems.Count];
        float[] yPosSave = new float[sceneItems.Count];
        float[] zPosSave = new float[sceneItems.Count];

        float[] xRotSave = new float[sceneItems.Count];
        float[] yRotSave = new float[sceneItems.Count];
        float[] zRotSave = new float[sceneItems.Count];

        int[] itemsIds = new int[sceneItems.Count];

        for (int i = 0; i < sceneItems.Count; i++)
        {
            xPosSave[i] = sceneItems[i].transform.position.x;
            yPosSave[i] = sceneItems[i].transform.position.y;
            zPosSave[i] = sceneItems[i].transform.position.z;

            xRotSave[i] = sceneItems[i].transform.rotation.x;
            yRotSave[i] = sceneItems[i].transform.rotation.y;
            zRotSave[i] = sceneItems[i].transform.rotation.z;

            itemsIds[i] = ItemList.FirstOrDefault(x => x.Value == sceneItems[i].ItemName).Key;
        }

        return new SaveData
        {
            XPos = xPosSave,
            YPos = yPosSave,
            ZPos = zPosSave,
            XRot = xRotSave,
            YRot = yRotSave,
            ZRot = zRotSave,
            ItemsID = itemsIds.ToList()
        };
    }

    public void RestoreState(object state)
    {
        var savedData = (SaveData)state;

        List<Item> itemsInScene = FindObjectsOfType<Item>().ToList();

        for (int i = 0; i < savedData.ItemsID.Count; i++)
        {
            
        }
    }

    private bool HaveItemInList(Item item, List<Item> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].ItemID == item.ItemID)
                return true;
        }

        return false;
    }

    [ContextMenu("Debug Dict")]
    private void DebugDict()
    {
        string d = "";

        foreach (KeyValuePair<int, string> entry in ItemList)
        {
            d+= entry.Key + ": " + entry.Value + "\n";
        }

        Debug.Log(d);
    }

    public GameObject ItemPrefabByItemName(string name)
    {
        SetupData();
        GameObject item = Resources.Load<GameObject>($"Items/{name}");

        return item;
    }

    public GameObject ItemPrefabByID(int id)
    {
        SetupData();
        string itemName = ItemList[id];
        GameObject item = Resources.Load<GameObject>($"Items/{itemName}");

        return item;
    }
}