using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomRedactor
{
    public abstract class LevelObject : ScriptableObject
    {
        [SerializeField] private CellObject _prefab;

        public virtual CellObject Prefab => _prefab;

        public abstract void Place(LevelData levelData, Vector2Int position, ObjectParameters parameters);
        public abstract void Remove(LevelData levelData, Vector2Int position, ObjectParameters parameters);
    }
}