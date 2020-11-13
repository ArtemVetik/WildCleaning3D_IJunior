using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using CustomRedactor;

[CustomEditor(typeof(LevelRedactor))]
public class RedactorEditor : Editor
{
    private LevelRedactor _levelRedactor;

    private void OnEnable()
    {
        _levelRedactor = (LevelRedactor)target;
    }

    private void OnSceneGUI()
    {
        
        Event e = Event.current;
        if (e.isMouse && e.button != 0)
            return;
        
        int controlID = GUIUtility.GetControlID(FocusType.Passive);
        switch (e.GetTypeForControl(controlID))
        {
            case EventType.MouseDown:
                GUIUtility.hotControl = controlID;
                Raycast();
                e.Use();
                EditorUtility.SetDirty(_levelRedactor.LevelDataBase);
                break;
            case EventType.MouseUp:
                GUIUtility.hotControl = 0;
                e.Use();
                break;
            case EventType.MouseDrag:
                GUIUtility.hotControl = controlID;
                Raycast();
                e.Use();
                EditorUtility.SetDirty(_levelRedactor.LevelDataBase);
                break;
        }
    }

    private void Raycast()
    {
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
            _levelRedactor.LeftMouseHandler(hitInfo);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ShowTitle();

        if (_levelRedactor.CurrentLevelData == null)
            return;

        int newWidth = EditorGUILayout.IntField("width", _levelRedactor.CurrentLevelData.Size.x, GUILayout.Width(0.5f * Screen.width));
        int newHeight = EditorGUILayout.IntField("height", _levelRedactor.CurrentLevelData.Size.y, GUILayout.Width(0.5f * Screen.width));

        _levelRedactor.CurrentLevelData.Size = new Vector2Int(newWidth, newHeight);

        GUILayout.Label("Выбор текущего игрового объекта");

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Пол", GUILayout.Width(0.1f * Screen.width)))
            _levelRedactor.SetCurrentObject<CustomRedactor.Empty>();
        if (GUILayout.Button("Игрок", GUILayout.Width(0.1f * Screen.width)))
            _levelRedactor.SetCurrentObject<CustomRedactor.Player>();
        if (GUILayout.Button("Микроб", GUILayout.Width(0.1f * Screen.width)))
            _levelRedactor.SetCurrentObject<CustomRedactor.Microbe>();
        if (GUILayout.Button("Вирус", GUILayout.Width(0.1f * Screen.width)))
            _levelRedactor.SetCurrentObject<CustomRedactor.Virus>();
        EditorGUILayout.EndHorizontal();

        if (GUI.changed)
            EditorUtility.SetDirty(_levelRedactor.LevelDataBase);
    }

    private void ShowTitle()
    {
        GUILayout.Label("Total levels: " + _levelRedactor.LevelDataBase.Count + " | Current level: " + (_levelRedactor.CurrentLevelIndex + 1).ToString());

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("<-", GUILayout.Width(0.1f * Screen.width)))
            _levelRedactor.CurrentLevelIndex--;
        if (GUILayout.Button("->", GUILayout.Width(0.1f * Screen.width)))
            _levelRedactor.CurrentLevelIndex++;
        if (GUILayout.Button("+", GUILayout.Width(0.1f * Screen.width)))
            _levelRedactor.LevelDataBase.Add();
        if (GUILayout.Button("-", GUILayout.Width(0.1f * Screen.width)))
            _levelRedactor.LevelDataBase.Remove(_levelRedactor.CurrentLevelData);
        EditorGUILayout.EndHorizontal();

        _levelRedactor.CurrentLevelIndex = Mathf.Clamp(_levelRedactor.CurrentLevelIndex, 0, _levelRedactor.LevelDataBase.Count - 1);
    }
}
