using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DailyBonusPresenterAnimation : MonoBehaviour
{
    public static class Parameters
    {
        public static readonly string ShowClear = nameof(ShowClear);
        public static readonly string ShowFocus = nameof(ShowFocus);
    }

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetTrigger(string name)
    {
        _animator.SetTrigger(name);
    }
}