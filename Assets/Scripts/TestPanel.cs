using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPanel : MonoBehaviour
{
    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();

        BoosterInventory boosterInventory = new BoosterInventory();
        boosterInventory.Save(new JsonSaveLoad());

        CleanerInventory cleanerInventory = new CleanerInventory();
        cleanerInventory.Save(new JsonSaveLoad());
    }
}
