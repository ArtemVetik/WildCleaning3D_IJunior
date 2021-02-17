using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterGameSlots : MonoBehaviour
{
    [SerializeField] private BoosterGameSlotsListView _gameSlots;

    private IEnumerable<BoosterData> _boosters;
    private IEnumerable<BoosterGameSlotPresenter> _presenters;
    private BoosterGameSlotPresenter _currentPresenter;

    public void SetBoosters(IEnumerable<BoosterData> boosters)
    {
        _boosters = boosters;
    }

    private void OnEnable()
    {
        if (_boosters != null)
        {
            _presenters = _gameSlots.Render(_boosters);
            foreach (var presenter in _presenters)
            {
                presenter.UseButtonClicked += OnUseButtonClicked;
                presenter.BoosterUsed += OnBoosterUsed;
            }
        }
    }

    private void OnUseButtonClicked(BoosterGameSlotPresenter presenter)
    {
        _currentPresenter = presenter;
    }

    private void OnBoosterUsed(BoosterGameSlotPresenter presenter)
    {
        _currentPresenter.Disable();
    }

    private void OnDisable()
    {
        if (_presenters != null)
        {
            foreach (var presenter in _presenters)
            {
                presenter.UseButtonClicked -= OnUseButtonClicked;
                presenter.BoosterUsed -= OnBoosterUsed;
                Destroy(presenter.gameObject);
            }
        }
    }
}
