using System;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectIdAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ScriptableObjectIdAttribute))]
public class ScriptableObjectIdDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        string assetPath = AssetDatabase.GetAssetPath(property.serializedObject.targetObject.GetInstanceID());
        string uniqueId = AssetDatabase.AssetPathToGUID(assetPath);

        property.stringValue = uniqueId;

        Rect textFieldPosition = position;
        textFieldPosition.height = 16;
        EditorGUI.PropertyField(textFieldPosition, property, label);
        GUI.enabled = true;
    }
}
#endif

public class BaseScriptableObject : ScriptableObject
{
    [ScriptableObjectId]
    [SerializeField]
    private string _guid;

    public string GUID => _guid;
}