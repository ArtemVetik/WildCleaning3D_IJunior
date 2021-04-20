using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using CustomRedactor;

namespace CustomRedactor
{
    public enum EditType
    {
        Add, Remove,
    }

    [ExecuteInEditMode]
    [RequireComponent(typeof(BoxCollider))]
    public class LevelRedactor : MonoBehaviour
    {
        [SerializeField] private LevelDataBase _levelDateBase;
        [SerializeField] private EditorObjectData _currentObject;
        [SerializeField] private Room[] _rooms;

        [SerializeField] public List<EditorObjectData> EditorObjects = new List<EditorObjectData>();

        [HideInInspector] public int CurrentLevelIndex;
        [HideInInspector] public bool ShowObjectParameters;
        [HideInInspector] public EditType EditType;

        private BoxCollider _collider;

        public LevelDataBase LevelDataBase => _levelDateBase;
        public LevelData CurrentLevelData => _levelDateBase.EmptyOrNull ? null : _levelDateBase[CurrentLevelIndex];

        private void OnDrawGizmos()
        {
            if (_levelDateBase.EmptyOrNull)
                return;

            InitCollider(_levelDateBase[CurrentLevelIndex].Size);
            DrawGrid(_levelDateBase[CurrentLevelIndex].Size, Color.gray);
            DrawObjects(_levelDateBase[CurrentLevelIndex].Map);
        }

        public void SetCurrentObject(EditorObjectData data)
        {
            _currentObject = data;
        }

        private void Start()
        {
            _rooms = FindObjectsOfType<Room>();
        }

        public void UpdateRoomDecor()
        {
            foreach (var room in _rooms)
                room.ShowDecorInEditor(CurrentLevelIndex + 1);
        }

        public void LeftMouseHandler(Vector2Int cell)
        {
            if (EditType == EditType.Add)
                _currentObject.LevelObject.Place(_levelDateBase[CurrentLevelIndex], cell, _currentObject.ObjectParameters);
            else if (EditType == EditType.Remove)
                _currentObject.LevelObject.Remove(LevelDataBase[CurrentLevelIndex], cell, _currentObject.ObjectParameters);
        }

        private void InitCollider(Vector2Int size)
        {
            if (_collider == null)
                _collider = GetComponent<BoxCollider>();

            _collider.center = new Vector3(size.x / 2f - 0.5f, 0, size.y / 2f - 0.5f);
            _collider.size = new Vector3(size.x, 0, size.y);
        }

        private void DrawGrid(Vector2Int size, Color color)
        {
            Color startColor = Gizmos.color;
            Gizmos.color = color;

            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    Gizmos.DrawWireCube(new Vector3(x, 0, y), new Vector3(1, 0, 1));
                }
            }

            Gizmos.color = startColor;
        }

        private void DrawObjects(LevelDictionary map)
        {
#if UNITY_EDITOR
            if (_levelDateBase[CurrentLevelIndex].Map == null)
                return;

            var textStyle = new GUIStyle();
            textStyle.normal.textColor = Color.yellow;
            textStyle.fontSize = 16;

            Gizmos.color = Color.white;
            foreach (Vector2Int cell in map.Keys)
            {
                Gizmos.DrawCube(cell.ToVector3(), new Vector3(0.9f, 0, 0.9f));
            }

            LevelObject levelObject;
            foreach (Vector2Int cell in map.Keys)
            {
                levelObject = map[cell].LevelObject;
                if (levelObject is Player)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(cell.ToVector3(0.4f), 0.4f);
                }
                else if (levelObject is Microbe)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(cell.ToVector3(0.4f), Vector3.one * 0.4f);
                }
                else if (levelObject is Virus)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawCube(cell.ToVector3(0.4f), Vector3.one * 0.4f);
                }
                else if (levelObject is Antiseptic)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawSphere(cell.ToVector3(0.4f), 0.25f);
                }
                else if (levelObject is Speedboost)
                {
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawSphere(cell.ToVector3(0.4f), 0.25f);
                }
                else if (levelObject is WatersBall)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawSphere(cell.ToVector3(0.4f), 0.25f);
                }

                var parameter = map[cell].Parameters;
                if (ShowObjectParameters && parameter != null)
                    Handles.Label(cell.ToVector3(1.5f), parameter.Name, textStyle);
            }
#endif
        }
    }
}