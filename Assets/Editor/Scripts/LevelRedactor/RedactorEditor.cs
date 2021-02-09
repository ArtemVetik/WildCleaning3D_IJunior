using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using CustomRedactor;
using System.Reflection;

public enum EditorMode
{
    PlaceObject, SetStages,
}

[CustomEditor(typeof(LevelRedactor))]
public class RedactorEditor : Editor
{
    private LevelRedactor _levelRedactor;
    private int _currentObjectIndex;
    private bool _mapSizeFoldout;
    private GameObject _previewGameObject;
    private Editor _previewGameObjectEditor;
    private EditorMode _editorMode;
    private int _setStagesIndex;

    private void OnEnable()
    {
        _levelRedactor = (LevelRedactor)target;

        _currentObjectIndex = 0;
        _mapSizeFoldout = true;
        _levelRedactor.EditType = EditType.Add;
        _editorMode = EditorMode.PlaceObject;
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
            case EventType.KeyDown:
                GUIUtility.keyboardControl = controlID;
                if (Event.current.keyCode == KeyCode.LeftShift)
                    _levelRedactor.EditType = EditType.Remove;
                break;
            case EventType.KeyUp:
                GUIUtility.keyboardControl = controlID;
                if (Event.current.keyCode == KeyCode.LeftShift)
                    _levelRedactor.EditType = EditType.Add;
                break;
        }
    }

    private void Raycast()
    {
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            int x = Mathf.RoundToInt(hitInfo.point.x);
            int y = Mathf.RoundToInt(hitInfo.point.z);
            Vector2Int cell = new Vector2Int(x, y);

            if (_editorMode == EditorMode.PlaceObject)
                _levelRedactor.LeftMouseHandler(cell);
            else if (_editorMode == EditorMode.SetStages)
            {
                _levelRedactor.CurrentLevelData.KeyStagesPoint[_setStagesIndex] = cell;
                _editorMode = EditorMode.PlaceObject;
            }
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ShowTitle();

        if (_levelRedactor.CurrentLevelData == null)
            return;

        SetMapSize();

        EditorGUILayout.HelpBox("ЛКМ->добавить объект\nShift + ЛКМ -> удалить объект", MessageType.Info);

        ShowCentralLabel("Выбор текущего игрового объекта", 16);
        EditorGUILayout.Space(10);
        ShowObjectButtons(100);
        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button(new GUIContent("+", "Добавить"), GUILayout.Width(50)))
            _levelRedactor.EditorObjects.Add(new EditorObjectData());
        if (GUILayout.Button(new GUIContent("-", "Удалить"), GUILayout.Width(50)))
        {
            if (_levelRedactor.EditorObjects.Count > 1)
            {
                _levelRedactor.EditorObjects.RemoveAt(_currentObjectIndex);
                _currentObjectIndex = 0;
            }
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(10);

        var currentEditorObject = serializedObject.FindProperty("EditorObjects").GetArrayElementAtIndex(_currentObjectIndex);
        ShowPropertyRelative(currentEditorObject, "_name");
        ShowPropertyRelative(currentEditorObject, "_levelObject");
        ShowPropertyRelative(currentEditorObject, "_objectParameter");

        if (_levelRedactor.EditorObjects[_currentObjectIndex].LevelObject == null)
            EditorGUILayout.HelpBox("Значение Level Object не должно быть пустым", MessageType.Error);
        else
            ShowGameObjectPreview(_levelRedactor.EditorObjects[_currentObjectIndex].LevelObject.Prefab.gameObject);

        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Добавить ключевую точку стадии");
        if (GUILayout.Button("+", GUILayout.Width(50)))
            _levelRedactor.CurrentLevelData.KeyStagesPoint.Add(new Vector2Int());
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(10);

        var vectorFieldStyle = new GUIStyle(GUI.skin.label);
        for (int pointIndex = 0; pointIndex < _levelRedactor.CurrentLevelData.KeyStagesPoint.Count; pointIndex++)
        {
            string labelText = (pointIndex + 1).ToString() + " стадия";
            if (_levelRedactor.CurrentLevelData.Map.Contains(_levelRedactor.CurrentLevelData.KeyStagesPoint[pointIndex]))
                vectorFieldStyle.normal.textColor = Color.black;
            else
                vectorFieldStyle.normal.textColor = Color.red;

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(labelText, vectorFieldStyle);
            GUILayout.FlexibleSpace();

            var newPoint = EditorGUILayout.Vector2IntField("", _levelRedactor.CurrentLevelData.KeyStagesPoint[pointIndex], GUILayout.Width(200));
            _levelRedactor.CurrentLevelData.KeyStagesPoint[pointIndex] = newPoint;

            Color savedColor = GUI.backgroundColor;
            if (_editorMode == EditorMode.SetStages && _setStagesIndex == pointIndex)
                GUI.backgroundColor = Color.blue;

            if (GUILayout.Button("set", GUILayout.Width(50)))
            {
                _editorMode = EditorMode.SetStages;
                _setStagesIndex = pointIndex;
            }
            GUI.backgroundColor = savedColor;

            if (GUILayout.Button("-", GUILayout.Width(50)))
                _levelRedactor.CurrentLevelData.KeyStagesPoint.RemoveAt(pointIndex);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
            EditorUtility.SetDirty(_levelRedactor.LevelDataBase);
    }

    private void DrawHorizontalLize(int height, Color color)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, height);
        rect.height = height;
        EditorGUI.DrawRect(rect, color);
    }

    private void ShowCentralLabel(string text, int fontSize = 12)
    {
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
        style.fontSize = fontSize;
        GUILayout.Label(text, style);
    }

    private void ShowLevelObjectButton(EditorObjectData data, float width)
    {
        Color savedColor = GUI.backgroundColor;

        if (_levelRedactor.EditorObjects[_currentObjectIndex].Equals(data))
            GUI.backgroundColor = Color.green;

        if (GUILayout.Button(data.Name, GUILayout.Width(width)))
        {
            _currentObjectIndex = _levelRedactor.EditorObjects.IndexOf(data);
            _levelRedactor.SetCurrentObject(data);
            _editorMode = EditorMode.PlaceObject;
        }

        GUI.backgroundColor = savedColor;
    }

    private void ShowTitle()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        string labelText = $"Total levels: {_levelRedactor.LevelDataBase.Count} | Current level: {(_levelRedactor.CurrentLevelIndex + 1).ToString()}";
        ShowCentralLabel(labelText, 14);

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button(new GUIContent("<-", "На уровень назад"), GUILayout.Width(50)))
            _levelRedactor.CurrentLevelIndex--;
        if (GUILayout.Button(new GUIContent("->", "На уровень вперед"), GUILayout.Width(50)))
            _levelRedactor.CurrentLevelIndex++;
        if (GUILayout.Button(new GUIContent("+", "Добавить уровень"), GUILayout.Width(50)))
            _levelRedactor.LevelDataBase.Add();
        if (GUILayout.Button(new GUIContent("-", "Удалить текущий уровень"), GUILayout.Width(50)))
        {
            bool delete = EditorUtility.DisplayDialog("Удалить текущий уровень?",
                "Текущий уровень будет полностью удален без возможности восстановления. Вы уверены?", "Удалить", "Не удалять");

            if (delete == true)
                _levelRedactor.LevelDataBase.Remove(_levelRedactor.CurrentLevelData);
            GUIUtility.ExitGUI();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        _levelRedactor.CurrentLevelIndex = Mathf.Clamp(_levelRedactor.CurrentLevelIndex, 0, _levelRedactor.LevelDataBase.Count - 1);
    }

    private void SetMapSize()
    {
        EditorGUILayout.BeginVertical();

        var foldoutStyle = new GUIStyle(EditorStyles.foldout);
        foldoutStyle.fontStyle = FontStyle.Bold;
        foldoutStyle.normal.textColor = Color.black;
        foldoutStyle.onNormal.textColor = Color.blue;

        _mapSizeFoldout = EditorGUILayout.Foldout(_mapSizeFoldout, "Размер карты", true, foldoutStyle);
        if (_mapSizeFoldout)
        {
            var saveIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel++;
            int newWidth = EditorGUILayout.IntSlider("ширина", _levelRedactor.CurrentLevelData.Size.x, 5, 100);
            int newHeight = EditorGUILayout.IntSlider("высота", _levelRedactor.CurrentLevelData.Size.y, 5, 100);
            EditorGUI.indentLevel = saveIndentLevel;

            _levelRedactor.CurrentLevelData.Size = new Vector2Int(newWidth, newHeight);
        }

        EditorGUILayout.EndVertical();
    }

    private void ShowObjectButtons(float width)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        for (int i = 0; i < _levelRedactor.EditorObjects.Count; i++)
        {
            ShowLevelObjectButton(_levelRedactor.EditorObjects[i], width);
            if ((i + 1) % 3 == 0)
            {
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
            }
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    private bool ShowPropertyRelative(SerializedProperty property, string propertyName)
    {
        var nestedProperty = property.FindPropertyRelative(propertyName);
        return EditorGUILayout.PropertyField(nestedProperty, true);
    }

    private void ShowGameObjectPreview(GameObject gameObject)
    {
        var nextObject = gameObject;
        if (_previewGameObject == null || nextObject.Equals(_previewGameObject) == false)
            _previewGameObjectEditor = Editor.CreateEditor(nextObject);

        _previewGameObject = nextObject;

        if (_previewGameObject != null)
            _previewGameObjectEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(128, 128), GUIStyle.none);
    }
}
