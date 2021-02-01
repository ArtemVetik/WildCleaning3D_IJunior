using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Add Booster Action", menuName = "Chests/Actions/Add Booster Action", order = 51)]
public class AddBoosterAction : ChestItemAction
{
    [SerializeField] private BoostersDataBase _dataBase;
    [SerializeField] private Booster _booster;

    public override void Use()
    {
        BoosterInventory inventory = new BoosterInventory(_dataBase);

        inventory.Load(new JsonSaveLoad());
        inventory.Add(_dataBase.FindFirst(_booster));
        inventory.Save(new JsonSaveLoad());
    }
}