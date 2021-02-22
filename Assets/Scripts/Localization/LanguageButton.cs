using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class LanguageButton : MonoBehaviour
{
    [SerializeField] private Image _checkIcon;
    [SerializeField] private string _language;

    private Button _button;

    public event UnityAction<LanguageButton> ButtonClicked;

    public string Language => _language;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke(this);
    }

    public void Select()
    {
        _checkIcon.enabled = true;
    }

    public void Deselect()
    {
        _checkIcon.enabled = false;
    }
}
