using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(PreviewSpriteAttribute))]
public class PreviewSpriteDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        if (property.objectReferenceValue != null && property.propertyType == SerializedPropertyType.ObjectReference)
        {
            Rect previewRect = new Rect(position.x, position.y, position.height, position.height);
            EditorGUI.DrawPreviewTexture(previewRect, ((Sprite)property.objectReferenceValue).texture);

            position.x += position.height + EditorGUIUtility.standardVerticalSpacing;
            position.width -= position.height + EditorGUIUtility.standardVerticalSpacing;
        }

        EditorGUI.PropertyField(position, property, label);

        EditorGUI.EndProperty();
    }
}