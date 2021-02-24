using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public struct DailyBonusData
{
    [SerializeField] private int _day;
    [SerializeField] private Sprite _preview;
    [SerializeField] private DailyBonusAction _action;

    public int Day => _day;
    public Sprite Preview => _preview;
    public int BonusValue => _action.BonusValue;
    public DailyBonusAction Action => _action;
}
