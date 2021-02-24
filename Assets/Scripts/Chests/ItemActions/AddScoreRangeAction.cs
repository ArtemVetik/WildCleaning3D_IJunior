using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Add Score Range Action", menuName = "Chests/Actions/Add Score Range Action", order = 51)]
public class AddScoreRangeAction : ChestItemAction
{
    [SerializeField] private int _minScoreValue;
    [SerializeField] private int _maxScoreValue;

    private int _rewardCount;

    private void OnValidate()
    {
        if (_minScoreValue > _maxScoreValue)
            _minScoreValue = _maxScoreValue;
    }

    public override void ApplyReward()
    {
        GoldBalance balance = new GoldBalance();
        balance.Load(new JsonSaveLoad());

        _rewardCount = Random.Range(_minScoreValue, _maxScoreValue + 1);
        balance.Add(_rewardCount);
        balance.Save(new JsonSaveLoad());
    }

    public override string GetRewardedText()
    {
        return string.Format("{0} Gold", _rewardCount);
    }
}
