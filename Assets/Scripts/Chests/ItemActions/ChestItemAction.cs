using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChestItemAction : ScriptableObject
{
    [SerializeField] private string _rewardedInfo;

    public string RewardedInfo => _rewardedInfo;

    public abstract void Use();
}
