using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CustomRedactor
{
    [CreateAssetMenu(fileName = "New Player", menuName = "Redactor/LevelObject/Player", order = 51)]
    public class Player : LevelObject
    {
        public override void Place(LevelData levelData, Vector2Int position, ObjectParameters parameters)
        {
            if (levelData.Map.ContainsKey(position))
            {
                DeleteAllPlayers(levelData);
                levelData.Map[position] = new CellData(this, parameters);
            }
        }

        public override void Remove(LevelData levelData, Vector2Int position, ObjectParameters parameters)
        {
            if (levelData.Map.ContainsKey(position) == false)
                return;

            if (levelData.Map[position].LevelObject is Player)
                levelData.Map[position] = new CellData();
        }

        private void DeleteAllPlayers(LevelData levelData)
        {
            foreach (Vector2Int cell in levelData.Map.Keys.ToArray())
            {
                if (levelData.Map[cell].LevelObject is Player)
                    levelData.Map[cell] = new CellData();
            }
        }
    }
}
