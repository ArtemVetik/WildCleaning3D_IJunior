using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using CustomRedactor;

[CustomEditor(typeof(LevelRedactor))]
public class RedactorEditor : Editor
{
    private LevelRedactor _levelRedactor;
    private SerializedProperty _levelObjects;
    private SerializedProperty _property;
    private UnityAction _setObjectAction;

    private void OnEnable()
    {
        _levelRedactor = (LevelRedactor)target;

        _property = serializedObject.FindProperty("_parameter");
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
        //serializedObject.Update();

        //_levelObjects = serializedObject.FindProperty("_levelObjects");
        //while (true)
        //{
        //    var myRect = GUILayoutUtility.GetRect(0f, 16f);
        //    var showChildren = EditorGUI.PropertyField(myRect, _levelObjects);
        //    if (_levelObjects.NextVisible(showChildren) == false)
        //        break;
        //}


        //serializedObject.ApplyModifiedProperties();

        //base.OnInspectorGUI();

        ShowTitle();

        if (_levelRedactor.CurrentLevelData == null)
            return;

        int newWidth = EditorGUILayout.IntField("width", _levelRedactor.CurrentLevelData.Size.x, GUILayout.Width(0.5f * Screen.width));
        int newHeight = EditorGUILayout.IntField("height", _levelRedactor.CurrentLevelData.Size.y, GUILayout.Width(0.5f * Screen.width));

        _levelRedactor.CurrentLevelData.Size = new Vector2Int(newWidth, newHeight);

        GUILayout.Label("Выбор текущего игрового объекта");

        EditorGUILayout.BeginHorizontal();
        GUILevelObjectButton<CustomRedactor.Empty>("Пол", 0.1f * Screen.width);
        GUILevelObjectButton<CustomRedactor.Player>("Игрок", 0.1f * Screen.width);
        GUILevelObjectButton<CustomRedactor.Microbe>("Микроб", 0.1f * Screen.width);
        GUILevelObjectButton<CustomRedactor.Virus>("Вирус", 0.1f * Screen.width);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("ПОЛ");

        EditorGUILayout.PropertyField(_property, new GUIContent("Parameter"));

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Добавить", GUILayout.Width(0.1f * Screen.width)))
        {
            _setObjectAction?.Invoke();
            _levelRedactor.EditType = EditType.Add;
        }
        if (GUILayout.Button("Удалить", GUILayout.Width(0.1f * Screen.width)))
        {
            _setObjectAction?.Invoke();
            _levelRedactor.EditType = EditType.Remove;
        }
        EditorGUILayout.EndHorizontal();

        if (GUI.changed)
            EditorUtility.SetDirty(_levelRedactor.LevelDataBase);
    }

    private void GUILevelObjectButton<T>(string name, float width) where T : LevelObject
    {
        if (GUILayout.Button(name, GUILayout.Width(width)))
        {
            _setObjectAction = delegate { _levelRedactor.SetCurrentObject<T>(); };
            _levelRedactor.EditType = EditType.None;
        }
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
