using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DailyBonusAction : ScriptableObject
{
    public abstract int BonusValue { get; }
    public abstract void AddBonus();
}
