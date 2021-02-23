using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Add Booster Action", menuName = "Chests/Actions/Add Booster Action", order = 51)]
public class AddBoosterAction : ChestItemAction
{
    [SerializeField] private BoostersDataBase _dataBase;
    [SerializeField] private Booster _booster;

    private BoosterData _data;

    public override void ApplyReward()
    {
        BoosterInventory inventory = new BoosterInventory(_dataBase);
        inventory.Load(new JsonSaveLoad());

        _data = _dataBase.FindFirst(_booster);
        inventory.Add(_data);
        inventory.Save(new JsonSaveLoad());
    }

    public override string GetRewardedText()
    {
        return _data.Name;
    }
}