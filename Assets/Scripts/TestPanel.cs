using System.Collections;
using System.Collections.Generic;
using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : MonoBehaviour
{
    [SerializeField] private BoostersDataBase _boosterDataBase;
    [SerializeField] private CleanersDataBase _cleanerDataBase;
    [SerializeField] private ChestDataBase _chestDataBase;
    [SerializeField] private InputField _scoreField;
    [SerializeField] private InputField _diamondField;
    [SerializeField] private InputField _levelField;
    [SerializeField] private GameObject[] _rooms;
    [SerializeField] private Chest _levelChest;

    private int _currentRoom = 0;

    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        BoosterInventory boosterInventory = new BoosterInventory(_boosterDataBase);
        boosterInventory.Save(new JsonSaveLoad());

        CleanerInventory cleanerInventory = new CleanerInventory(_cleanerDataBase);
        cleanerInventory.Add(_cleanerDataBase.DefaultData);
        cleanerInventory.SelectCleaner(_cleanerDataBase.DefaultData);
        cleanerInventory.Save(new JsonSaveLoad());

        foreach (var item in _cleanerDataBase.Data)
        {
            item.Prefab.Cleaner.DefaultCharacteristics.Load(new JsonSaveLoad());
            item.Prefab.Cleaner.DefaultCharacteristics.Save(new JsonSaveLoad());
        }
    }

    public void SetScore()
    {
        int score = int.Parse(_scoreField.text);

        GoldBalance balance = new GoldBalance();
        balance.Add(score);

        balance.Save(new JsonSaveLoad());
    }

    public void SetDiamond()
    {
        int score = int.Parse(_diamondField.text);

        DiamondBalance balance = new DiamondBalance();
        balance.Add(score);

        balance.Save(new JsonSaveLoad());
    }

    public void AddLevelChest()
    {
        ChestInventory inventory = new ChestInventory(_chestDataBase);
        inventory.Load(new JsonSaveLoad());
        inventory.Add(_levelChest);
        inventory.Save(new JsonSaveLoad());
    }

    public void SetLevel()
    {
        int level = int.Parse(_levelField.text);

        PlayerPrefs.SetInt("CurrentLevelNumber", level - 1);
        GameScene.Load();
    }

    public void NextRoom()
    {
        if (_currentRoom + 1 >= _rooms.Length)
            return;

        _rooms[_currentRoom].SetActive(false);
        _rooms[++_currentRoom].SetActive(true);
    }

    public void PreviousRoom()
    {
        if (_currentRoom - 1 < 0)
            return;

        _rooms[_currentRoom].SetActive(false);
        _rooms[--_currentRoom].SetActive(true);
    }
}
