using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class PopupPanel : MonoBehaviour
{
    public class AnimationParameters
    {
        public static readonly string Close = nameof(Close);
    }

    [SerializeField] private Button _closeButton;

    private Animator _animator;

    public event UnityAction<PopupPanel> Opened;
    public event UnityAction<PopupPanel> Closed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Close);
        Opened?.Invoke(this);
    }

    public void Close()
    {
        _animator.SetTrigger(AnimationParameters.Close);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(Close);
        Closed?.Invoke(this);
    }
}
