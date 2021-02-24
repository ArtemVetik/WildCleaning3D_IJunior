using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _day;
    [SerializeField] private Image _preview;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private GameObject _completeGroup;

    public void Render(DailyBonusData data)
    {
        _day.text = $"Day {data.Day}";
        _preview.sprite = data.Preview;
        _value.text = data.BonusValue.ToString();

        _completeGroup.SetActive(false);
    }

    public void SetPassingState()
    {
        _completeGroup.SetActive(true);
    }
}