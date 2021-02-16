using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageListView : MonoBehaviour
{
    [SerializeField] private StagePresenter _template;
    [SerializeField] private Transform _container;
    [SerializeField] private LevelStages _stages;

    private List<StagePresenter> _presenters;
    private StagePresenter _currentPresenter;

    private void OnEnable()
    {
        _presenters = new List<StagePresenter>();
        for (int i = 0; i < _stages.StageCount; i++)
        {
            var presenter = Instantiate(_template, _container);
            presenter.Render(i + 1);
            _presenters.Add(presenter);
        }
        _currentPresenter = _presenters[0];

        _stages.StageCompeted += OnStageCompleted;
        _stages.CompleteRateChanged += OnCompleteRateChanged;
    }

    private void OnCompleteRateChanged(float rate)
    {
        _currentPresenter.SetRate(rate);
    }

    private void OnStageCompleted(int stage)
    {
        _currentPresenter.RenderComplete();

        var nextStage = stage + 1;
        if (nextStage >= _stages.StageCount)
            return;

        _currentPresenter = _presenters[nextStage];
    }

    private void OnDisable()
    {
        _stages.StageCompeted -= OnStageCompleted;
        _stages.CompleteRateChanged -= OnCompleteRateChanged;
    }
}
