using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TabButtonAnimation : MonoBehaviour
{
    public class Parameters
    {
        public static readonly string Active = nameof(Active);
        public static readonly string Inactive = nameof(Inactive);
    }

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetTrigger(string parameter)
    {
        _animator.SetTrigger(parameter);
    }
}
