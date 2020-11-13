using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomRedactor
{
    [CreateAssetMenu(fileName = "New Empty", menuName = "Redactor/LevelObject/Empty", order = 51)]
    public class Empty : LevelObject
    {
        public override void Place(LevelData levelData, Vector2Int position, ObjectParameters parameters)
        {
            if (levelData.Map.ContainsKey(position))
                levelData.Map[position] = new CellData();
            else
                levelData.Map.Add(position, new CellData());
        }

        public override void Remove(LevelData levelData, Vector2Int position, ObjectParameters parameters)
        {
            if (levelData.Map.ContainsKey(position))
                levelData.Map.Remove(position);
        }
    }
}