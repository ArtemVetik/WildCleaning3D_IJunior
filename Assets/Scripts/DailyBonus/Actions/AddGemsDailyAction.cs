using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Add Gems Daily Action", menuName = "DailyBonus/NewAddGemsAction", order = 51)]
public class AddGemsDailyAction : DailyBonusAction
{
    [SerializeField] private int _gemsValue;

    public override int BonusValue => _gemsValue;

    public override void AddBonus()
    {
        DiamondBalance diamond = new DiamondBalance();
        diamond.Load(new JsonSaveLoad());
        diamond.Add(_gemsValue);
        diamond.Save(new JsonSaveLoad());
    }
}
