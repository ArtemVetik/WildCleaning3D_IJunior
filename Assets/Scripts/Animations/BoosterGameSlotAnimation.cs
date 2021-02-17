using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BoosterGameSlotAnimation : MonoBehaviour
{
    public static class Parameters
    {
        public static readonly string Hide = nameof(Hide);
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
