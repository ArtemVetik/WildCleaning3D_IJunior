using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(PopupPanel))]
public class TodayRewardPanel : MonoBehaviour
{
    [SerializeField] private Image _preview;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private TMP_Text _day;

    private PopupPanel _panel;

    public event UnityAction Closed;

    private void Awake()
    {
        _panel = GetComponent<PopupPanel>();
    }

    private void OnEnable()
    {
        _panel.Closed += OnPopupClosed;
    }

    private void OnDisable()
    {
        _panel.Closed -= OnPopupClosed;
    }

    private void OnPopupClosed(PopupPanel popup)
    {
        Closed?.Invoke();
    }

    public void Render(DailyBonusData data)
    {
        _preview.sprite = data.Preview;
        _value.text = data.BonusValue.ToString();
        _day.text = $"Day{data.Day}";
    }
}
