using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(BoosterGameSlotAnimation))]
public class BoosterGameSlotPresenter : MonoBehaviour
{
    [SerializeField] private Image _preview;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Button _useButton;

    private BoosterGameSlotAnimation _animation;
    private BoosterData _data;


    public event UnityAction<BoosterGameSlotPresenter> UseButtonClicked;
    public event UnityAction<BoosterGameSlotPresenter> BoosterUsed;

    private void Awake()
    {
        _animation = GetComponent<BoosterGameSlotAnimation>();
    }

    private void OnEnable()
    {
        _useButton.onClick.AddListener(OnUseButtonClick);
    }

    private void OnDisable()
    {
        _useButton.onClick.RemoveListener(OnUseButtonClick);

        if (_data != null)
            _data.Booster.Used -= OnBoosterUsed;
    }

    public void Render(BoosterData data)
    {
        _data = data;
        _data.Booster.Used += OnBoosterUsed;

        _preview.sprite = data.Preview;
        _name.text = LocalizationManager.Localize(data.Name);
    }

    private void OnBoosterUsed(Booster booster)
    {
        BoosterUsed?.Invoke(this);
    }

    public void Disable()
    {
        _animation.SetTrigger(BoosterGameSlotAnimation.Parameters.Hide);
    }

    private void OnUseButtonClick()
    {
        UseButtonClicked?.Invoke(this);
        _data.Booster.Use();
    }
}
