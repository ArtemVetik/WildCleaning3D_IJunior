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
    [SerializeField] private CellButton _cellButton;
    [SerializeField] private Animator _animator;

    private DiamondBalance _diamond;

    public int ChestPrice => _chest.Price;

    private void OnEnable()
    {
        _cellButton.ButtonClicked += OnBuyButtonClicked;
        _cellButton.Enabled += OnCellButtonEnable;
    }

    private void OnDisable()
    {
        _cellButton.ButtonClicked -= OnBuyButtonClicked;
        _cellButton.Enabled -= OnCellButtonEnable;
    }

    private void Start()
    {
        ChestInventory chestInventory = new ChestInventory(_dataBase);
        chestInventory.Load(new JsonSaveLoad());

        _name.text = _chest.Name;
        _inStock.text = $"In stock: {chestInventory.GetCount(_chest)}";
        _description.text = _chest.Description;

        _diamond = new DiamondBalance();
        _diamond.Load(new JsonSaveLoad());

        _cellButton.RenderPrice(ChestPrice, _diamond.Balance < ChestPrice);
    }

    private void OnCellButtonEnable(CellButton button)
    {
        _diamond.Load(new JsonSaveLoad());
        _cellButton.RenderPrice(ChestPrice, _diamond.Balance < ChestPrice);
    }

    private void OnBuyButtonClicked(CellButton button)
    {
        if (SpendMoney() == false)
            return;

        ChestInventory inventory = new ChestInventory(_dataBase);
        inventory.Load(new JsonSaveLoad());

        inventory.Add(_chest);
        inventory.Save(new JsonSaveLoad());

        _cellButton.RenderPrice(ChestPrice, _diamond.Balance < ChestPrice);

        _inStock.text = $"In stock: {inventory.GetCount(_chest)}";
        _animator.SetTrigger("Buyed"); // TODO: animation constant
    }

    private bool SpendMoney()
    {
        _diamond = new DiamondBalance();
        _diamond.Load(new JsonSaveLoad());

        if (_diamond.Balance < ChestPrice)
            return false;

        _diamond.SpendDiamond(ChestPrice);
        _diamond.Save(new JsonSaveLoad());
        return true;
    }
}
