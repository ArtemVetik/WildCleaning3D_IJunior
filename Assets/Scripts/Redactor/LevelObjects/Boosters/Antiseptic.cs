using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomRedactor
{
    [CreateAssetMenu(fileName = "New Antiseptic", menuName = "Redactor/LevelObject/Booster/Antiseptic", order = 51)]
    public class Antiseptic : Booster
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

            if (levelData.Map[position].LevelObject is Antiseptic)
                levelData.Map[position] = new CellData();
        }
    }
}