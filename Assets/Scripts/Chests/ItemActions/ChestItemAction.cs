using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChestItemAction : ScriptableObject
{
    [SerializeField] private ShowRewardAnimation _showEffect;
    [SerializeField] private string _rewardedInfo;

    public ShowRewardAnimation ShowEffect => _showEffect;
    public string RewardedInfo => _rewardedInfo;

    public abstract void Use();
}
