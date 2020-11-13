using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CustomRedactor
{
    public enum EditType
    {
        Add, Remove
    }

    [ExecuteInEditMode]
    [RequireComponent(typeof(BoxCollider))]
    public class LevelRedactor : MonoBehaviour
    {
        [SerializeField] private LevelDataBase _levelDateBase;
        [SerializeField] private LevelObject[] _levelObjects;
        [SerializeField] private EditType _editType;
        [SerializeField] private ObjectParameters _parameter;

        [HideInInspector] public int CurrentLevelIndex;

        private LevelObject _currentObject;
        private BoxCollider _collider;

        public LevelDataBase LevelDataBase => _levelDateBase;
        public LevelData CurrentLevelData => _levelDateBase.EmptyOrNull ? null : _levelDateBase[CurrentLevelIndex];

        private void OnDrawGizmos()
        {
            if (_levelDateBase.EmptyOrNull)
                return;

            InitCollider(_levelDateBase[CurrentLevelIndex].Size);
            DrawGrid(_levelDateBase[CurrentLevelIndex].Size, Color.gray);
            DrawObjects();
        }

        public void SetCurrentObject<T>() where T : LevelObject
        {
            foreach (LevelObject levelObject in _levelObjects)
            {
                if (levelObject is T)
                {
                    _currentObject = levelObject;
                    return;
                }
            }
        }

        public void LeftMouseHandler(RaycastHit hitInfo)
        {
            int x = Mathf.RoundToInt(hitInfo.point.x);
            int y = Mathf.RoundToInt(hitInfo.point.z);
            Vector2Int cell = new Vector2Int(x, y);

            if (_editType == EditType.Add)
                _currentObject.Place(_levelDateBase[CurrentLevelIndex], cell, _parameter);
            else if (_editType == EditType.Remove)
                _currentObject.Remove(LevelDataBase[CurrentLevelIndex], cell, _parameter);
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

        private void DrawObjects()
        {
            if (_levelDateBase[CurrentLevelIndex].Map == null)
                return;

            Gizmos.color = Color.white;
            foreach (Vector2Int cell in _levelDateBase[CurrentLevelIndex].Map.Keys)
            {
                Gizmos.DrawCube(cell.ToVector3(), new Vector3(0.9f, 0, 0.9f));
            }

            LevelObject levelObject;
            foreach (Vector2Int cell in _levelDateBase[CurrentLevelIndex].Map.Keys)
            {
                levelObject = _levelDateBase[CurrentLevelIndex].Map[cell].LevelObject;
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
            }
        }
    }
}