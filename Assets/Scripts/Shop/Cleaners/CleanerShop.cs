using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerShop : MonoBehaviour
{
    [SerializeField] private CleanersDataBase _dataBase;
    [SerializeField] private CleanerListView _cleanerListView;

    private IEnumerable<CleanerPresenter> _presenters;
    private CleanerInventory _inventory;

    private void OnEnable()
    {
        _inventory = new CleanerInventory();
        _inventory.Load(new JsonSaveLoad());

        if (_inventory.Contains(_dataBase.DefaultData) == false)
        {
            _inventory.Add(_dataBase.DefaultData);
            _inventory.SelectCleaner(_dataBase.DefaultData);
            _inventory.Save(new JsonSaveLoad());
        }

        _presenters = _cleanerListView.Render(_dataBase.Data);

        UpdateView();
        InitPResenterEvents();
    }

    private void InitPResenterEvents()
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
    }

    private void OnCellButtonClicked(CleanerPresenter presenter)
    {
        _inventory.Add(presenter.Data);
        _inventory.Save(new JsonSaveLoad());

        presenter.RenderBuyed(presenter.Data);
    }

    private void OnDisable()
    {
        RemovePresenterEvents();
        foreach (var presenter in _presenters)
            Destroy(presenter.gameObject);
    }
}
