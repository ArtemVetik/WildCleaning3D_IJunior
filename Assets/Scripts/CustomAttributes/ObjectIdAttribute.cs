using System;
using UnityEditor;
using UnityEngine;

public class ObjectIdAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ObjectIdAttribute))]
public class ObjectIdDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        string assetPath = AssetDatabase.GetAssetPath(property.serializedObject.targetObject.GetInstanceID());
        string uniqueId = AssetDatabase.AssetPathToGUID(assetPath);
        uniqueId = Guid.NewGuid().ToString();
        if (string.IsNullOrEmpty(property.stringValue))
            property.stringValue = uniqueId;

        Rect textFieldPosition = position;
        textFieldPosition.height = 16;
        EditorGUI.PropertyField(textFieldPosition, property, label);
        GUI.enabled = true;
    }
}
#endif