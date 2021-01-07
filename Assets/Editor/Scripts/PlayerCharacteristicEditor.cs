using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerCharacteristics))]
public class PlayerCharacteristicEditor : Editor
{
    private PlayerCharacteristics _characteristics;
    private bool _modifiedParametersFoldout;
    private PlayerData _modifiedData;

    private void OnEnable()
    {
        _characteristics = target as PlayerCharacteristics;

        _modifiedData = new PlayerData();
        _modifiedData.Load(new JsonSaveLoad());
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _modifiedParametersFoldout = EditorGUILayout.Foldout(_modifiedParametersFoldout, "ModifiedParameters", true);
        if (_modifiedParametersFoldout)
        {
            GUI.enabled = false;
            var saveIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel++;

            EditorGUILayout.FloatField("Speed", _modifiedData.Speed);

            EditorGUI.indentLevel = saveIndentLevel;
            GUI.enabled = true;
        }

    }
}
