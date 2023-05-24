using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item), true)]
public class ItemEditor : Editor
{
    Item item;

    private void OnEnable()
    {
        item = target as Item;
    }

    public override void OnInspectorGUI()
    {
        if(item.ItemIcon == null)
        {
            base.OnInspectorGUI();
            return;
        }

        Texture2D texture = AssetPreview.GetAssetPreview(item.ItemIcon);
        GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        base.OnInspectorGUI();
    }
}