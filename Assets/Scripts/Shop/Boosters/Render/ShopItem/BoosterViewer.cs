using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BoosterViewer : ShopViewer
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    [SerializeField] private CellButton _cellButton;

    private List<BoosterShopPresenter> _presenters;
    private BoosterShopPresenter _currentPresenter;

    private void OnEnable()
    {
        _nextButton.onClick.AddListener(OnNextButtonClicked);
        _previousButton.onClick.AddListener(OnPreviousButtonClicked);
    }

    private void OnDisable()
    {
        _nextButton.onClick.RemoveListener(OnNextButtonClicked);
        _previousButton.onClick.RemoveListener(OnPreviousButtonClicked);
    }

    public void InitPresenters(IEnumerable<BoosterShopPresenter> presenters)
    {
        _presenters = new List<BoosterShopPresenter>(presenters);
        SetPresenter(_presenters.First());
    }

    private void SetPresenter(BoosterShopPresenter presenter)
    {
        if (_currentPresenter)
            _currentPresenter.RemoveButtonsEvent(_cellButton);

        SetCameraTarget(presenter.transform);
        _currentPresenter = presenter;
        _currentPresenter.InitButtonsEvent(_cellButton);

        ScoreBalance score = new ScoreBalance();
        score.Load(new JsonSaveLoad());

        _cellButton.RenderPrice(_currentPresenter.BoosterPrice, score.Balance < presenter.BoosterPrice);

        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        foreach (var presenter in _presenters)
            presenter.Animation.StopAnimation(CleanerPresenterAnimation.Parameters.Present);

        _currentPresenter.Animation.PlayAnimation(CleanerPresenterAnimation.Parameters.Present);
    }

    private void OnNextButtonClicked()
    {
        int currentIndex = _presenters.IndexOf(_currentPresenter);
        if (currentIndex >= _presenters.Count - 1)
            return;

        SetPresenter(_presenters[currentIndex + 1]);
    }

    private void OnPreviousButtonClicked()
    {
        int currentIndex = _presenters.IndexOf(_currentPresenter);
        if (currentIndex <= 0)
            return;

        SetPresenter(_presenters[currentIndex - 1]);
    }
}
