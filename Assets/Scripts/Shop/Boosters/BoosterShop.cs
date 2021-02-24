using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterShop : MonoBehaviour
{
    [SerializeField] private BoostersDataBase _boosterDataBase;
    [SerializeField] private BoosterShopListView _boostersListView;
    [SerializeField] private BoosterViewer _boosterViewer;

    private BoosterInventory _inventory;
    private IEnumerable<BoosterShopPresenter> _boosterPresenters;

    private void OnEnable()
    {
        _inventory = new BoosterInventory(_boosterDataBase);
        _inventory.Load(new JsonSaveLoad());

        _boosterPresenters = _boostersListView.Render(_boosterDataBase.Data);
        InitSellButtons(_boosterPresenters);
        _boosterViewer.InitPresenters(_boosterPresenters);
    }

    private void OnSellButtonClicked(BoosterShopPresenter presenter)
    {
        GoldBalance money = new GoldBalance();
        money.Load(new JsonSaveLoad());

        if (money.Balance < presenter.BoosterPrice)
            return;

        money.SpendScore(presenter.BoosterPrice);
        money.Save(new JsonSaveLoad());

        _inventory.Add(presenter.Data);
        _inventory.Save(new JsonSaveLoad());

        presenter.UpdateView();
        presenter.Animation.PlayTrigger(BoosterPresenterAnimation.Parameters.Buyed);
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
        RemoveSellButtons(_boosterPresenters);

        foreach (BoosterShopPresenter presenter in _boosterPresenters)
            if (presenter)
                Destroy(presenter.gameObject);
    }
}
