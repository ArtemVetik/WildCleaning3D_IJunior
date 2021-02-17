using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterGameSlots : MonoBehaviour
{
    [SerializeField] private BoosterGameSlotsListView _gameSlots;

    private IEnumerable<BoosterData> _boosters;
    private IEnumerable<BoosterGameSlotPresenter> _presenters;
    private BoosterGameSlotPresenter _currentUse;

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
                presenter.Data.Booster.Used += OnBoosterUsed;
            }
        }
    }

    private void OnBoosterUsed(Booster booster)
    {
        _currentUse.Disable();

        _currentUse.UseButtonClicked -= OnUseButtonClicked;
        _currentUse.Data.Booster.Used -= OnBoosterUsed;
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
                if (presenter == null)
                    continue;

                presenter.UseButtonClicked -= OnUseButtonClicked;
                presenter.Data.Booster.Used -= OnBoosterUsed;
                Destroy(presenter.gameObject);
            }
        }
    }
}
