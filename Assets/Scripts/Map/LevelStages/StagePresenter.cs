using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StagePresenter : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _stageNumber;
    [SerializeField] private Image _stageImage;
    [SerializeField] private Sprite _completeSprite;

    public void Render(int stageNumber)
    {
        _stageNumber.text = stageNumber.ToString();
        _slider.value = 0f;
    }

    public void SetRate(float value)
    {
        _slider.value = value;
    }

    public void RenderComplete()
    {
        _stageImage.sprite = _completeSprite;
    }
}
