using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraAnimations : MonoBehaviour
{
    public static class Parameters
    {
        public static readonly string DeadShake = nameof(DeadShake);
        public static readonly string StartFilling = nameof(StartFilling);
        public static readonly string EndFilling = nameof(EndFilling);
        public static readonly string CompleteLevelLoop = nameof(CompleteLevelLoop);
    }

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetTrigger(string value)
    {
        _animator.SetTrigger(value);
    }
}
