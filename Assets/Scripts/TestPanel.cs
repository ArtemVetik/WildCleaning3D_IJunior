using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : MonoBehaviour
{
    [SerializeField] private BoostersDataBase _boosterDataBase;
    [SerializeField] private CleanersDataBase _cleanerDataBase;
    [SerializeField] private InputField _scoreField;

    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();

        BoosterInventory boosterInventory = new BoosterInventory(_boosterDataBase);
        boosterInventory.Save(new JsonSaveLoad());

        CleanerInventory cleanerInventory = new CleanerInventory(_cleanerDataBase);
        cleanerInventory.Save(new JsonSaveLoad());
    }

    public void SetScore()
    {
        int score = int.Parse(_scoreField.text);

        ScoreBalance balance = new ScoreBalance();
        balance.AddScore(score);

        balance.Save(new JsonSaveLoad());
    }
}
