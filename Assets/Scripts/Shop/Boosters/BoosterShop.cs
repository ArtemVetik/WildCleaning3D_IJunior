using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterShop : MonoBehaviour
{
    [SerializeField] private BoostersDataBase _boosterDataBase;
    [SerializeField] private BoosterShopListView _boostersListView;

    private BoosterInventory _inventory;
    private IEnumerable<BoosterShopPresenter> _boosterPresenters;

    private void OnEnable()
    {
        _inventory = new BoosterInventory();
        _inventory.Load(new JsonSaveLoad());

        _boosterPresenters = _boostersListView.Render(_boosterDataBase.Data);
        InitSellButtons(_boosterPresenters);
    }

    private void OnSellButtonClicked(BoosterShopPresenter presenter)
    {
        _inventory.Add(presenter.Data);
    }

    private void InitSellButtons(IEnumerable<BoosterShopPresenter> boosterPresenters)
    {
        foreach (BoosterShopPresenter presenter in boosterPresenters)
        {
            presenter.SellButtonClicked += OnSellButtonClicked;
        }
    }

    private void RemoveSellButtons(IEnumerable<BoosterShopPresenter> boosterPresenters)
    {
        foreach (BoosterShopPresenter presenter in boosterPresenters)
        {
            presenter.SellButtonClicked -= OnSellButtonClicked;
        }
    }

    private void OnDisable()
    {
        _inventory.Save(new JsonSaveLoad());
        RemoveSellButtons(_boosterPresenters);

        foreach (BoosterShopPresenter presenter in _boosterPresenters)
            Destroy(presenter.gameObject);
    }
}
