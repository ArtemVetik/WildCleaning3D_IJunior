using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Add Gold Daily Action", menuName = "DailyBonus/NewAddGoldAction", order = 51)]
public class AddGoldDailyAction : DailyBonusAction
{
    [SerializeField] private int _goldValue;

    public override int BonusValue => _goldValue;

    public override void AddBonus()
    {
        GoldBalance gold = new GoldBalance();
        gold.Load(new JsonSaveLoad());
        gold.Add(_goldValue);
        gold.Save(new JsonSaveLoad());
    }
}