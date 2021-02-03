using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class CleanerPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_FontAsset _lockAsset;
    [SerializeField] private TMP_FontAsset _unlockAsset;
    [SerializeField] private TMP_FontAsset _selectedAsset;

    private GameObject _model;

    public CleanerData Data { get; private set; }

    public event UnityAction<CleanerPresenter> CellButtonClicked;
    public event UnityAction<CleanerPresenter> SelectButtonClicked;

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
            _model = Instantiate(data.Prefab.Cleaner.gameObject, transform.position, Quaternion.Euler(0, 180, 0), transform);

        _name.text = data.Name;
        _description.text = data.Description;

        _name.font = _lockAsset;
    }

    public void RenderBuyed(CleanerData data)
    {
        Data = data;

        _name.text = data.Name;
        _description.text = data.Description;

        _name.font = _unlockAsset;
    }

    public void RenderSelected(CleanerData data)
    {
        Data = data;

        _name.text = data.Name;
        _description.text = data.Description;

        _name.font = _selectedAsset;
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
