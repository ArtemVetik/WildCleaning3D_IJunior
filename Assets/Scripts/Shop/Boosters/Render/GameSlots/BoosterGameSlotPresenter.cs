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

    public BoosterData Data { get; private set; }

    public event UnityAction<BoosterGameSlotPresenter> UseButtonClicked;

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
    }

    public void Render(BoosterData data)
    {
        Data = data;

        _preview.sprite = data.Preview;
        _name.text = data.Name;
    }

    public void Disable()
    {
        _animation.SetTrigger(BoosterGameSlotAnimation.Parameters.Hide);
    }

    private void OnUseButtonClick()
    {
        UseButtonClicked?.Invoke(this);
    }
}
