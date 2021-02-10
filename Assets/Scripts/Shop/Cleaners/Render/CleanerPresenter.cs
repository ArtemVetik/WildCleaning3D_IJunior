using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CleanerPresenterAnimation))]
public class CleanerPresenter : MonoBehaviour
{
    [SerializeField] private Transform _modelContainer;
    [SerializeField] private Transform _effectsContainer;
    [SerializeField] private GameObject _glassBox;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _speed;
    [SerializeField] private TMP_Text _cleanliness;
    [SerializeField] private TMP_FontAsset _lockAsset;
    [SerializeField] private TMP_FontAsset _unlockAsset;
    [SerializeField] private TMP_FontAsset _selectedAsset;

    private GameObject _model;

    public Transform EffectsContainer => _effectsContainer;
    public CleanerPresenterAnimation Animation { get; private set; }
    public CleanerData Data { get; private set; }

    public event UnityAction<CleanerPresenter> CellButtonClicked;
    public event UnityAction<CleanerPresenter> SelectButtonClicked;

    private void Awake()
    {
        Animation = GetComponent<CleanerPresenterAnimation>();
    }

    public void InitButtonsEvent(Button cellButton, Button selectButton)
    {
        cellButton.onClick.AddListener(OnCellButtonClick);
        selectButton.onClick.AddListener(OnSelectButtonClick);
    }

    public void RemoveButtonsEvent(Button cellButton, Button selectButton)
    {
        cellButton.onClick.RemoveListener(OnCellButtonClick);
        selectButton.onClick.RemoveListener(OnSelectButtonClick);
    }

    public void Render(CleanerData data)
    {
        Data = data;

        if (_model == null)
        {
            _model = Instantiate(data.EmptyModel, _modelContainer);
            _model.transform.rotation = Quaternion.Euler(0, 90, 0);
            _model.transform.localPosition = Vector3.zero;
        }

        _name.text = data.Name;
        _description.text = data.Description;

        var characteristics = Data.Prefab.Cleaner.DefaultCharacteristics;
        characteristics.Load(new JsonSaveLoad());

        _speed.text = "Speed: " + string.Format("{0:f2}", characteristics.Speed);
        _cleanliness.text = "Cleanliness: " + string.Format("{0:P2}", characteristics.Cleanliness);

        _name.font = _lockAsset;
    }

    public void RenderBuyed(CleanerData data)
    {
        Data = data;

        _name.text = data.Name;
        _description.text = data.Description;

        _name.font = _unlockAsset;
        _glassBox.SetActive(false);
    }

    public void RenderSelected(CleanerData data)
    {
        Data = data;

        _name.text = data.Name;
        _description.text = data.Description;

        _name.font = _selectedAsset;
        _glassBox.SetActive(false);
    }

    private void OnCellButtonClick()
    {
        CellButtonClicked?.Invoke(this);
    }

    private void OnSelectButtonClick()
    {
        SelectButtonClicked?.Invoke(this);
    }
}
