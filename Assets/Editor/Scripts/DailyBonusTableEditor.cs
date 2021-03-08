using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
using System;

[CustomEditor(typeof(DailyBonusTable))]
public class DailyBonusTableEditor : Editor
{
    private DailyBonusTable _table;
    private SerializedProperty _dataBaseList;
    private List<FieldInfo> _fieldNames;
    private string _dataBaseFieldName;

    private void Awake()
    {
        _table = target as DailyBonusTable;

        _fieldNames = new List<FieldInfo>();
        foreach (var field in typeof(DailyBonusData).GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            _fieldNames.Add(field);

        FieldInfo[] allFields = _table.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        _dataBaseFieldName = allFields.ToList().Find((field) => field.FieldType.IsEquivalentTo(typeof(List<DailyBonusData>))).Name;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        _dataBaseList = serializedObject.FindProperty(_dataBaseFieldName);

        for (int i = 0; i < _dataBaseList.arraySize; i++)
        {
            SerializedProperty element = _dataBaseList.GetArrayElementAtIndex(i);
            foreach (var item in _fieldNames)
            {
                var serializableField = element.FindPropertyRelative(item.Name);
                if (item.FieldType == typeof(int))
                {
                    GUI.enabled = false;
                    serializableField.intValue = i + 1;
                    EditorGUILayout.PropertyField(serializableField);
                    GUI.enabled = true;
                }
                else if (item.FieldType == typeof(Sprite))
                {
                    var rect = new Rect(0, (i-1) * 10, 150, 150);
                    serializableField.objectReferenceValue = EditorGUI.ObjectField(rect, serializableField.objectReferenceValue, typeof(Texture2D), false);
                }
                else
                {
                    EditorGUILayout.PropertyField(serializableField);
                }
            }

            if (GUILayout.Button(new GUIContent("-", "Удалить")))
                _table.RemoveAt(i);
        }

        if (GUILayout.Button(new GUIContent("+", "Добавить")))
            _table.Add(new DailyBonusData());

        serializedObject.ApplyModifiedProperties();
    }
}
