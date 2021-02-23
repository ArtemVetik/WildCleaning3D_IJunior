using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Add Booster Range Action", menuName = "Chests/Actions/Add Booster Range Action", order = 51)]
public class AddBoosterRange : ChestItemAction
{
    [SerializeField] private BoostersDataBase _dataBase;
    [SerializeField] private Booster _booster;
    [SerializeField] private int _minCount;
    [SerializeField] private int _maxCount;

    private BoosterData _data;
    private int _rewardCount;

    public override void ApplyReward()
    {
        BoosterInventory inventory = new BoosterInventory(_dataBase);

        inventory.Load(new JsonSaveLoad());
        _data = _dataBase.FindFirst(_booster);
        _rewardCount = Random.Range(_minCount, _maxCount + 1);

        for (int i = 0; i < _rewardCount; i++)
            inventory.Add(_data);

        inventory.Save(new JsonSaveLoad());
    }

    public override string GetRewardedText()
    {
        return string.Format("{0} X{1}", _data.Name, _rewardCount);
    }
}
