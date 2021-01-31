using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyChestPresenter : MonoBehaviour
{
    [SerializeField] private ChestDataBase _dataBase;
    [SerializeField] private Chest _chest;
    [SerializeField] private Text _name;
    [SerializeField] private Image _preview;
    [SerializeField] private Button _buyButton;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
    }

    private void Start()
    {
        _name.text = _chest.Name;
        _preview.sprite = _chest.Preview;
    }

    private void OnBuyButtonClicked()
    {
        ChestInventory inventory = new ChestInventory(_dataBase);

        inventory.Load(new JsonSaveLoad());
        inventory.Add(_chest);
        inventory.Save(new JsonSaveLoad());
    }
}
