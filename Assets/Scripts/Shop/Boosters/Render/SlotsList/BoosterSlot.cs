using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoosterSlot : MonoBehaviour
{
    [SerializeField] private Image _preview;
    [SerializeField] private Text _name;
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _removeButton;

    public BoosterData? Data { get; private set; }

    public event UnityAction<BoosterSlot> AddButtonClicked;
    public event UnityAction<BoosterSlot, BoosterData> RemoveButtonClicked;

    private void OnEnable()
    {
        _addButton.onClick.AddListener(OnAddButtonClick);
        _removeButton.onClick.AddListener(OnRemoveButtonClick);

        Data = null;
    }

    private void OnDisable()
    {
        _addButton.onClick.RemoveListener(OnAddButtonClick);
        _removeButton.onClick.RemoveListener(OnAddButtonClick);
    }

    public void SetData(BoosterData data)
    {
        Data = data;

        _preview.sprite = data.Preview;
        _name.text = data.Name;
    }

    private void OnAddButtonClick()
    {
        if (Data != null)
            return;

        AddButtonClicked?.Invoke(this);
    }

    private void OnRemoveButtonClick()
    {
        if (Data == null)
            return;

        RemoveButtonClicked?.Invoke(this, Data.Value);

        Data = null;
        SetDefaultView();
    }

    private void SetDefaultView()
    {
        _preview.sprite = null;
        _name.text = "";
    }
}
