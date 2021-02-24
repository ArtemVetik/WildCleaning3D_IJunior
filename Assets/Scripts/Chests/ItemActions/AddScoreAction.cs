using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Add Score Action", menuName = "Chests/Actions/Add Score Action", order = 51)]
public class AddScoreAction : ChestItemAction
{
    [SerializeField] private int _scoreValue;

    public override void ApplyReward()
    {
        GoldBalance balance = new GoldBalance();
        balance.Load(new JsonSaveLoad());
        balance.Add(_scoreValue);
        balance.Save(new JsonSaveLoad());
    }

    public override string GetRewardedText()
    {
        return string.Format("{0} Score", _scoreValue);
    }
}
