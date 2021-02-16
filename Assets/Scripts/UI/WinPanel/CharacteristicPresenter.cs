using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _upgrageText;
    [SerializeField] private TMP_Text _sliderText;
    [SerializeField] private Slider _slider;

    public void Render(float value, float maxValue, float upgradeValue)
    {
        _sliderText.text = $"{value:N2}/{maxValue:N2}";
        _upgrageText.text = $"+{upgradeValue:N2}";

        _slider.value = value / maxValue;
    }
}
