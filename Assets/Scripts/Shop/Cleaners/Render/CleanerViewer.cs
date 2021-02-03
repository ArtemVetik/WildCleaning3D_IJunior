using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanerViewer : MonoBehaviour
{
    [SerializeField] private CleanersDataBase _dataBase;
    [SerializeField] private CameraTargetFolowing _cameraFolowing;
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

    private void Start()
    {
        _inventory = new CleanerInventory(_dataBase);
        _inventory.Load(new JsonSaveLoad());
    }

    public void InitPresenters(IEnumerable<CleanerPresenter> presenters)
    {
        _presenters = new List<CleanerPresenter>(presenters);
        SetPresenter(_presenters[0], true);
    }

    private void SetPresenter(CleanerPresenter presenter, bool isPurchased)
    {
        if (_currentPresenter)
            _currentPresenter.RemoveButtonsEvent(_cellButton, _selectButton);

        _cameraFolowing.SetTarget(presenter.transform);
        _currentPresenter = presenter;
        _currentPresenter.InitButtonsEvent(_cellButton, _selectButton);

        _cellButton.gameObject.SetActive(!isPurchased);
        _selectButton.gameObject.SetActive(isPurchased);
    }

    private void OnNextButtonClicked()
    {
        int currentIndex = _presenters.IndexOf(_currentPresenter);
        if (currentIndex >= _presenters.Count - 1)
            return;

        bool purchased = _inventory.Contains(_currentPresenter.Data);
        SetPresenter(_presenters[currentIndex + 1], purchased);
    }

    private void OnPreviousButtonClicked()
    {
        int currentIndex = _presenters.IndexOf(_currentPresenter);
        if (currentIndex <= 0)
            return;

        bool purchased = _inventory.Contains(_currentPresenter.Data);
        SetPresenter(_presenters[currentIndex - 1], purchased);
    }
}
