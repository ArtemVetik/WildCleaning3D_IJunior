using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCleanerDisposablePanel : DisposablePanel
{
    [SerializeField] private CleanersDataBase _dataBase;
    [SerializeField] private PlayerCharacteristics[] _characteristics;

    public override bool TryOpen(int currentLevel)
    {
        CleanerInventory inventory = new CleanerInventory(_dataBase);
        inventory.Load(new JsonSaveLoad());

        foreach (var charanteristic in _characteristics)
        {
            var playerData = charanteristic.Characteristic;
            if (inventory.Contains(playerData))
                return false;
        }

        return base.TryOpen(currentLevel);
    }
}
