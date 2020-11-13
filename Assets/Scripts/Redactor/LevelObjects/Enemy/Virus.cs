using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomRedactor
{
    [CreateAssetMenu(fileName = "New Virus", menuName = "Redactor/LevelObject/Enemy/Virus", order = 51)]
    public class Virus : Enemy
    {
        public override void Place(LevelData levelData, Vector2Int position, ObjectParameters parameters)
        {
            if (levelData.Map.ContainsKey(position))
                levelData.Map[position] = new CellData(this, parameters);
        }

        public override void Remove(LevelData levelData, Vector2Int position, ObjectParameters parameters)
        {
            if (levelData.Map.ContainsKey(position) == false)
                return;

            if (levelData.Map[position].LevelObject is Virus)
                levelData.Map[position] = new CellData();
        }
    }
}