using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(TabButtonAnimation))]
public class TabButton : MonoBehaviour
{
    private Button _button;
    private TabButtonAnimation _animation;

    public event UnityAction<TabButton> ButtonClicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _animation = GetComponent<TabButtonAnimation>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked() 
    {
        ButtonClicked?.Invoke(this);
    }

    public void SetActive()
    {
        _animation.SetTrigger(TabButtonAnimation.Parameters.Active);
    }

    public void SetInactive()
    {
        _animation.SetTrigger(TabButtonAnimation.Parameters.Inactive);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }
}
