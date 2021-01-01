using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterGameSlots : MonoBehaviour
{
    [SerializeField] private BoosterGameSlotsListView _gameSlots;

    private IEnumerable<BoosterGameSlotPresenter> _presenters;
    private BoosterGameSlotPresenter _currentUse;

    public void SetBoosters(IEnumerable<BoosterData> boosters)
    {
        _presenters = _gameSlots.Render(boosters);
        foreach (var presenter in _presenters)
        {
            presenter.UseButtonClicked += OnUseButtonClicked;
            presenter.Data.Booster.Used += OnBoosterUsed;
        }
    }

    private void OnBoosterUsed(Booster booster)
    {
        _currentUse.Disable();
    }

    private void OnUseButtonClicked(BoosterGameSlotPresenter presenter)
    {
        _currentUse = presenter;
        presenter.Data.Booster.Use();
    }

    private void OnDisable()
    {
        if (_presenters != null)
        {
            foreach (var presenter in _presenters)
            {
                presenter.UseButtonClicked -= OnUseButtonClicked;
                presenter.Data.Booster.Used -= OnBoosterUsed;
                Destroy(presenter.gameObject);
            }
        }
    }
}
