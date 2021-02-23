using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CleanerShop : MonoBehaviour
{
    [SerializeField] private CleanersDataBase _dataBase;
    [SerializeField] private ParticleSystem _buyedEffect;
    [SerializeField] private CleanerListView _cleanerListView;
    [SerializeField] private CleanerViewer _cleanerViewer;

    private IEnumerable<CleanerPresenter> _presenters;
    private CleanerInventory _inventory;

    private void OnEnable()
    {
        _inventory = new CleanerInventory(_dataBase);
        _inventory.Load(new JsonSaveLoad());

        if (_inventory.Contains(_dataBase.DefaultData) == false)
        {
            _inventory.Add(_dataBase.DefaultData);
            _inventory.SelectCleaner(_dataBase.DefaultData);
            _inventory.Save(new JsonSaveLoad());
        }

        _presenters = _cleanerListView.Render(_dataBase.Data);
        _cleanerViewer.InitPresenters(_presenters);

        UpdateView();
        InitPresenterEvents();
    }

    private void InitPresenterEvents()
    {
        foreach (var presenter in _presenters)
        {
            presenter.CellButtonClicked += OnCellButtonClicked;
            presenter.SelectButtonClicked += OnSelectedButtonClicked;
        }
    }

    private void RemovePresenterEvents()
    {
        foreach (var presenter in _presenters)
        {
            presenter.CellButtonClicked -= OnCellButtonClicked;
            presenter.SelectButtonClicked -= OnSelectedButtonClicked;
        }
    }

    private void UpdateView()
    {
        foreach (var presenter in _presenters)
        {
            if (_inventory.Contains(presenter.Data))
                presenter.RenderBuyed(presenter.Data);

            if (_inventory.SelectedCleaner.Equals(presenter.Data))
                presenter.RenderSelected(presenter.Data);
        }
    }

    private void OnSelectedButtonClicked(CleanerPresenter presenter)
    {
        _inventory.SelectCleaner(presenter.Data);
        _inventory.Save(new JsonSaveLoad());

        UpdateView();
        presenter.Animation.PlayTrigger(CleanerPresenterAnimation.Parameters.Selected);
    }

    private void OnCellButtonClicked(CleanerPresenter presenter)
    {
        if (SpendBalance(presenter.CleanerPrice) == false)
            return;

        _inventory.Add(presenter.Data);
        _inventory.Save(new JsonSaveLoad());

        presenter.RenderBuyed(presenter.Data);
        Instantiate(_buyedEffect, presenter.EffectsContainer);

        _cleanerViewer.UpdateUI();
    }

    private bool SpendBalance(int value)
    {
        DiamondBalance diamond = new DiamondBalance();
        diamond.Load(new JsonSaveLoad());

        bool spend = diamond.SpendDiamond(value);
        diamond.Save(new JsonSaveLoad());

        return spend;
    }

    private void OnDisable()
    {
        RemovePresenterEvents();
        foreach (var presenter in _presenters)
            if (presenter)
                Destroy(presenter.gameObject);
    }
}
