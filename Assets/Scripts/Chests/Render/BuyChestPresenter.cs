using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyChestPresenter : MonoBehaviour
{
    [SerializeField] private ChestDataBase _dataBase;
    [SerializeField] private Chest _chest;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _inStock;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Animator _animator;

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
        ChestInventory chestInventory = new ChestInventory(_dataBase);
        chestInventory.Load(new JsonSaveLoad());

        _name.text = _chest.Name;
        _inStock.text = $"In stock: {chestInventory.GetCount(_chest)}";
        _description.text = _chest.Description;
    }

    private void OnBuyButtonClicked()
    {
        ChestInventory inventory = new ChestInventory(_dataBase);

        inventory.Load(new JsonSaveLoad());
        inventory.Add(_chest);
        inventory.Save(new JsonSaveLoad());

        _inStock.text = $"In stock: {inventory.GetCount(_chest)}";
        _animator.SetTrigger("Buyed"); // TODO: replace
    }
}
