﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CleanerPresenterAnimation : MonoBehaviour
{
    public static class Parameters
    {
        public static string Present = nameof(Present);
    }

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string parameter)
    {
        _animator.SetBool(parameter, true);
    }

    public void StopAnimation(string parameter)
    {
        _animator.SetBool(parameter, false);
    }
}