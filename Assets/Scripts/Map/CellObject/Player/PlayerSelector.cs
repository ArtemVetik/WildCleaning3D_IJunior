using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerOnLevelDictionary : SerializableDictionary<int, CleanerPrefab> { }

[CreateAssetMenu(fileName = "New Player Selector", menuName = "Player/PlayerSelector", order = 51)]
public class PlayerSelector : ScriptableObject
{
    [SerializeField] private CleanersDataBase _dataBase;
    [SerializeField] private PlayerOnLevelDictionary _players;

    public bool HasInDictionary(Player player)
    {
        foreach (var playerInDictionart in _players.Values)
        {
            if (playerInDictionart.Cleaner.DefaultCharacteristics.ID == player.DefaultCharacteristics.ID)
                return true;
        }

        return false;
    }

    public Player GetPlayer()
    {
        var levelLoader = FindObjectOfType<CurrentLevelLoader>();
        int levelNumber = levelLoader.LevelIndex + 1;
        if (_players.Keys.Contains(levelNumber))
            return _players[levelNumber].Cleaner;

        var inventory = new CleanerInventory(_dataBase);
        inventory.Load(new JsonSaveLoad());

        if (inventory.SelectedCleaner == null)
            inventory.SelectCleaner(_dataBase.DefaultData);

        return inventory.SelectedCleaner.Prefab.Cleaner;
    }
}
