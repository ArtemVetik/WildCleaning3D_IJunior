using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPanel : MonoBehaviour
{
    [SerializeField] private BoostersDataBase _boosterDataBase;
    [SerializeField] private CleanersDataBase _cleanerDataBase;

    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();

        BoosterInventory boosterInventory = new BoosterInventory(_boosterDataBase);
        boosterInventory.Save(new JsonSaveLoad());

        CleanerInventory cleanerInventory = new CleanerInventory(_cleanerDataBase);
        cleanerInventory.Save(new JsonSaveLoad());
    }
}
