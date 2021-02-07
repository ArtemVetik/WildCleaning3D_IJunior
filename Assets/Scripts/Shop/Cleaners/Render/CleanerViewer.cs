using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanerViewer : ShopViewer
{
    [SerializeField] private CleanersDataBase _dataBase;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    [SerializeField] private Button _cellButton;
    [SerializeField] private Button _selectButton;

    private List<CleanerPresenter> _presenters;
    private CleanerPresenter _currentPresenter;
    private CleanerInventory _inventory;

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

    public void InitPresenters(IEnumerable<CleanerPresenter> presenters)
    {
        _presenters = new List<CleanerPresenter>(presenters);

        _inventory = new CleanerInventory(_dataBase);
        _inventory.Load(new JsonSaveLoad());

        var selectedPresenter = _presenters.Find(presenter => presenter.Data.Equals(_inventory.SelectedCleaner));
        SetPresenter(selectedPresenter);
    }

    public void UpdateUI()
    {
        _inventory.Load(new JsonSaveLoad());
        bool purchased = _inventory.Contains(_currentPresenter.Data);
        _cellButton.gameObject.SetActive(!purchased);
        _selectButton.gameObject.SetActive(purchased);
    }

    private void SetPresenter(CleanerPresenter presenter)
    {
        if (_currentPresenter)
            _currentPresenter.RemoveButtonsEvent(_cellButton, _selectButton);

        SetCameraTarget(presenter.transform);
        _currentPresenter = presenter;
        _currentPresenter.InitButtonsEvent(_cellButton, _selectButton);

        UpdateAnimations();
        UpdateUI();
    }

    private void UpdateAnimations()
    {
        foreach (var presenter in _presenters)
            presenter.Animation.StopAnimation(CleanerPresenterAnimation.Parameters.Present);

        _currentPresenter.Animation.PlayAnimation(CleanerPresenterAnimation.Parameters.Present);

        _inventory.Load(new JsonSaveLoad());
        if (_inventory.Contains(_currentPresenter.Data))
            _currentPresenter.Animation.PlayAnimation(CleanerPresenterAnimation.Parameters.Buyed);
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
