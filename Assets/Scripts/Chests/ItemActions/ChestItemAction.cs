using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChestItemAction : ScriptableObject
{
    [SerializeField] private GameObject _showEffect;
    [SerializeField] private string _rewardedInfo;

    public GameObject ShowEffect => _showEffect;
    public string RewardedInfo => _rewardedInfo;

    public abstract void Use();
}
