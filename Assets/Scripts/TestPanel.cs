using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : MonoBehaviour
{
    [SerializeField] private BoostersDataBase _boosterDataBase;
    [SerializeField] private CleanersDataBase _cleanerDataBase;
    [SerializeField] private InputField _scoreField;
    [SerializeField] private InputField _levelField;

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

    }

    public void SetScore()
    {
        int score = int.Parse(_scoreField.text);

        ScoreBalance balance = new ScoreBalance();
        balance.AddScore(score);

        balance.Save(new JsonSaveLoad());
    }

    public void SetLevel()
    {
        int level = int.Parse(_levelField.text);

        PlayerPrefs.SetInt("CurrentLevelNumber", level);
    }
}
