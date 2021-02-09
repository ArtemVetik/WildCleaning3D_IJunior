using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class PlayerOnLevelDictionary : SerializableDictionary<int, CleanerPrefab> { }

[CreateAssetMenu(fileName = "New Player Selector", menuName = "Player/PlayerSelector", order = 51)]
public class PlayerSelector : ScriptableObject
{
    [SerializeField] private CleanersDataBase _dataBase;
    [SerializeField] private PlayerOnLevelDictionary _players;

    public Player GetPlayer()
    {
        var levelLoader = FindObjectOfType<CurrentLevelLoader>();
        int levelNumber = levelLoader.LevelIndex;
        if (_players.Keys.Contains(levelNumber))
            return _players[levelNumber].Cleaner;

        var inventory = new CleanerInventory(_dataBase);
        inventory.Load(new JsonSaveLoad());

        return inventory.SelectedCleaner.Prefab.Cleaner;
    }
}
