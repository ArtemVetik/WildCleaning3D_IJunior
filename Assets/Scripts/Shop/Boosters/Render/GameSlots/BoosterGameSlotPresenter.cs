using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoosterGameSlotPresenter : MonoBehaviour
{
    [SerializeField] private Image _preview;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Button _useButton;

    public BoosterData Data { get; private set; }

    public event UnityAction<BoosterGameSlotPresenter> UseButtonClicked;

    private void OnEnable()
    {
        _useButton.onClick.AddListener(OnUseButtonClick);
    }

    private void OnDisable()
    {
        _useButton.onClick.RemoveListener(OnUseButtonClick);
    }

    public void Render(BoosterData data)
    {
        Data = data;

        _preview.sprite = data.Preview;
        _name.text = data.Name;
    }

    public void Disable()
    {
        _preview.sprite = null;
        _name.text = "";
        _useButton.interactable = false;
    }

    private void OnUseButtonClick()
    {
        UseButtonClicked?.Invoke(this);
    }
}
