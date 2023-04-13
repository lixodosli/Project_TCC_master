using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;

public class ItemSaver : MonoBehaviour, ISaveable
{
    public static ItemSaver Instance;

    [Header("Items Data File")]
    [SerializeField] private TextAsset m_Data;
    [Header("List")]
    [SerializeField] private List<R_Item> m_ItemsInScene = new List<R_Item>();
    private List<string> _ItemsInSceneSaveState = new List<string>();

    private Dictionary<string, object> _ItemList = new Dictionary<string, object>();

    private void Awake()
    {
        Instance = this;
        SetupItems();
    }

    [ContextMenu("Setup Items")]
    private void SetupItems()
    {
        m_ItemsInScene.Clear();
        m_ItemsInScene = FindObjectsOfType<R_Item>().ToList();
        _ItemList = SheetsReader.ReadJSON<Dictionary<string, object>>(m_Data.text);

        for (int i = 0; i < m_ItemsInScene.Count; i++)
        {
            _ItemsInSceneSaveState.Add(m_ItemsInScene[i].ItemID);
        }
    }

    private void UpdateItemInScene(R_Item item)
    {
        if (item.gameObject.activeSelf != item.SaveState.IsActive)
            item.gameObject.SetActive(item.SaveState.IsActive);

        if (item.transform.parent != item.SaveState.ItemParent)
            item.transform.parent = item.SaveState.ItemParent;
    }

    [System.Serializable]
    private struct SaveData
    {
        public float[] xPos;
        public float[] yPos;
        public float[] zPos;

        public float[] xRot;
        public float[] yRot;
        public float[] zRot;

        public List<string> Items;
    }

    public object CaptureState()
    {
        float[] xPosSave = new float[m_ItemsInScene.Count];
        float[] yPosSave = new float[m_ItemsInScene.Count];
        float[] zPosSave = new float[m_ItemsInScene.Count];

        float[] xRotSave = new float[m_ItemsInScene.Count];
        float[] yRotSave = new float[m_ItemsInScene.Count];
        float[] zRotSave = new float[m_ItemsInScene.Count];

        for (int i = 0; i < m_ItemsInScene.Count; i++)
        {
            xPosSave[i] = m_ItemsInScene[i].transform.position.x;
            yPosSave[i] = m_ItemsInScene[i].transform.position.y;
            zPosSave[i] = m_ItemsInScene[i].transform.position.z;

            xRotSave[i] = m_ItemsInScene[i].transform.rotation.x;
            yRotSave[i] = m_ItemsInScene[i].transform.rotation.y;
            zRotSave[i] = m_ItemsInScene[i].transform.rotation.z;
        }

        return new SaveData
        {
            xPos = xPosSave,
            yPos = yPosSave,
            zPos = zPosSave,
            xRot = xRotSave,
            yRot = yRotSave,
            zRot = zRotSave,
            Items = _ItemsInSceneSaveState
        };
    }

    public void RestoreState(object state)
    {
        var savedData = (SaveData)state;

        List<R_Item> savedItems = new List<R_Item>();

        foreach (var item in _ItemsInSceneSaveState)
        {
            //savedItems.Add(item.Item);
        }

        List<R_Item> currentItems = FindObjectsOfType<R_Item>().ToList();

        // Achar os items salvos que tambem estao na cena atual
        for (int i = 0; i < savedItems.Count; i++)
        {
            if(HaveItemInList(savedItems[i], currentItems))
            {
                // restaura o item
                savedItems[i].transform.SetPositionAndRotation(new Vector3(savedData.xPos[i], savedData.yPos[i], savedData.zPos[i]), Quaternion.Euler(savedData.xRot[i], savedData.yRot[i], savedData.zRot[i]));
                UpdateItemInScene(savedItems[i]);
            }
            else
            {
                // adiciona o item
                GameObject addedItem = Instantiate(savedItems[i].gameObject, new Vector3(savedData.xPos[i], savedData.yPos[i], savedData.zPos[i]), Quaternion.Euler(savedData.xRot[i], savedData.yRot[i], savedData.zRot[i]));
                UpdateItemInScene(addedItem.GetComponent<R_Item>());
            }
        }

        // Achar os items que estao no cenario, mas n estao no save
        for (int i = 0; i < currentItems.Count; i++)
        {
            if(!HaveItemInList(currentItems[i], savedItems))
            {
                // destruir o item
                currentItems[i].transform.parent = null;
                currentItems[i].gameObject.SetActive(false);
                Debug.Log("<" + currentItems[i].name + "> nao esta no save.");
            }
        }
    }

    private bool HaveItemInList(R_Item item, List<R_Item> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].ItemID == item.ItemID)
                return true;
        }

        return false;
    }
}