using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class ShowRewardAnimation : MonoBehaviour
{
    private Animator _animator;

    public event UnityAction<ShowRewardAnimation> RewardShown;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetTrigger(string name)
    {
        _animator.SetTrigger(name);
    }

    private void InvokeShownEvent()
    {
        RewardShown?.Invoke(this);
    }

}
