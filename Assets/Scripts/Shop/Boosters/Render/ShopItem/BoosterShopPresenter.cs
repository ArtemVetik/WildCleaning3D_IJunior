using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(BoosterPresenterAnimation))]
public class BoosterShopPresenter : MonoBehaviour
{
    [SerializeField] private Transform _modelContainer;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _inStock;
    [SerializeField] private TMP_Text _description;

    private BoosterInventory _inventory;
    private GameObject _model;

    public BoosterData Data { get; private set; }
    public int BoosterPrice => Data.Price;

    public UnityAction<BoosterShopPresenter> SellButtonClicked;
    public BoosterPresenterAnimation Animation { get; private set; }

    private void Awake()
    {
        Animation = GetComponent<BoosterPresenterAnimation>();
    }

    public void InitButtonsEvent(CellButton cellButton)
    {
        cellButton.ButtonClicked += OnCellButtonClick;
    }

    public void RemoveButtonsEvent(CellButton cellButton)
    {
        cellButton.ButtonClicked -= OnCellButtonClick;
    }

    public void Render(BoosterData data, BoosterInventory inventory)
    {
        Data = data;
        _inventory = inventory;

        if (_model == null)
        {
            _model = Instantiate(data.EmptyModel, _modelContainer);
            _model.transform.localPosition = Vector3.zero;
        }

        _name.text = data.Name;
        _description.text = data.Description;

        UpdateView();
    }

    public void UpdateView()
    {
        _inventory.Load(new JsonSaveLoad());

        int inStockCount = _inventory.Data.Where(booster => booster.GUID == Data.GUID).Count();
        _inStock.text = $"In stock: {inStockCount.ToString()}";
    }

    public void OnCellButtonClick(CellButton cellButton)
    {
        SellButtonClicked?.Invoke(this);
    }
}
