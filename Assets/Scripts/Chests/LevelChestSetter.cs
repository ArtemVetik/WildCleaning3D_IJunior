using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChestSetter : MonoBehaviour
{
    private const string LevelChestKey = "LevelChestLevel";

    [SerializeField] private ChestDataBase _dataBase;
    [SerializeField] private CurrentLevelLoader _levelLoader;
    [SerializeField] private EndLevelTrigger _endTrigger;
    [SerializeField] private Chest _chest;

    private int _lastLevelChestIndex;

    private void OnEnable()
    {
        _endTrigger.LevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        _endTrigger.LevelCompleted -= OnLevelCompleted;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(LevelChestKey) == false)
            PlayerPrefs.SetInt(LevelChestKey, 0);

        _lastLevelChestIndex = PlayerPrefs.GetInt(LevelChestKey);
    }

    private void OnLevelCompleted()
    {
        if (_levelLoader.LevelIndex % 5 != 0)
            return;

        if (_levelLoader.LevelIndex == _lastLevelChestIndex)
            return;

        ChestInventory inventory = new ChestInventory(_dataBase);
        inventory.Load(new JsonSaveLoad());
        inventory.Add(_chest);
        inventory.Save(new JsonSaveLoad());
        Debug.Log("Add chest");
        PlayerPrefs.SetInt(LevelChestKey, _levelLoader.LevelIndex);
    }
}
