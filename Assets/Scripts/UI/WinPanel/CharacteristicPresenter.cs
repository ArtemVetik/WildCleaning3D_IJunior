using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicPresenter : MonoBehaviour
{
    [SerializeField] private bool _inPercents;
    [SerializeField] private TMP_Text _upgrageText;
    [SerializeField] private TMP_Text _sliderText;
    [SerializeField] private Slider _slider;

    public void Render(float value, float maxValue, float upgradeValue)
    {
        if (upgradeValue == 0)
            _upgrageText.gameObject.SetActive(false);

        if (upgradeValue > 0)
            _upgrageText.color = Color.green;
        else
            _upgrageText.color = Color.red;

        if (_inPercents)
        {
            _sliderText.text = string.Format("{0:0.##;0.##;0}%", (value / maxValue * 100));
            _upgrageText.text = string.Format("{0:+0.##;-0.##;0}%", upgradeValue * 100);
        }
        else
        {
            _sliderText.text = $"{value:0.##}/{maxValue:0.##}";
            _upgrageText.text = string.Format("{0:+0.00;-0.00;0}", upgradeValue);
        }

        _slider.value = value / maxValue;
    }
}
