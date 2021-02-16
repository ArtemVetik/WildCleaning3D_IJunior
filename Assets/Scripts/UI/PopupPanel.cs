using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PopupPanel : MonoBehaviour
{
    public class AnimationParameters
    {
        public static readonly string Close = nameof(Close);
    }

    [SerializeField] private Button _closeButton;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        _animator.SetTrigger(AnimationParameters.Close);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }
}
