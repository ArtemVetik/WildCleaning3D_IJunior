using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class ChestAnimation : MonoBehaviour
{
    public static class Parameters
    {
        public static readonly string Open = nameof(Open);
    }

    private Animator _animator;

    public event UnityAction ChestOpened;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetTrigger(string key)
    {
        _animator.SetTrigger(key);
    }

    private void OnChestOpened()
    {
        ChestOpened?.Invoke();
    }
}
