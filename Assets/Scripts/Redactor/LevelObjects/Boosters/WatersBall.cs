using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomRedactor
{
    [CreateAssetMenu(fileName = "New WatersBall", menuName = "Redactor/LevelObject/Booster/WatersBall", order = 51)]
    public class WatersBall : Booster
    {
        public override void Place(LevelData levelData, Vector2Int position, ObjectParameters parameters)
        {
            if (levelData.Map.ContainsKey(position))
                levelData.Map[position] = new CellData(this, null);
        }

        public override void Remove(LevelData levelData, Vector2Int position, ObjectParameters parameters)
        {
            if (levelData.Map.ContainsKey(position) == false)
                return;

            if (levelData.Map[position].LevelObject is WatersBall)
                levelData.Map[position] = new CellData();
        }
    }
}