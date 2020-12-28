using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoosterShopPresenter : MonoBehaviour
{
    [SerializeField] private Image _preview;
    [SerializeField] private Text _name;
    [SerializeField] private Text _desctiprion;
    [SerializeField] private Button _sellButton;

    public BoosterData Data { get; private set; }

    public UnityAction<BoosterShopPresenter> SellButtonClicked;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnSellButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnSellButtonClick);
    }

    public void Render(BoosterData data)
    {
        Data = data;

        _preview.sprite = data.Preview;
        _name.text = data.Name;
        _desctiprion.text = data.Description;
    }

    public void OnSellButtonClick()
    {
        SellButtonClicked?.Invoke(this);
    }
}
