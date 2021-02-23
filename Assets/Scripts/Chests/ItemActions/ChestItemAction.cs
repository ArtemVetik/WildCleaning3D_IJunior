using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChestItemAction : ScriptableObject
{
    [SerializeField] private ShowRewardAnimation _showEffect;

    public ShowRewardAnimation ShowEffect => _showEffect;
    public string RewardedText { get; private set; }

    public void Use()
    {
        ApplyReward();
        RewardedText = GetRewardedText();
    }

    public abstract void ApplyReward();
    public abstract string GetRewardedText();
}
