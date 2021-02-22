using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoosterMenuSlot : MonoBehaviour
{
    [SerializeField] private Sprite _defaultIcon;
    [SerializeField] private Sprite _lockIcon;
    [SerializeField] private Image _preview;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _removeButton;

    private bool _locked;
    private int _lockLevel;

    public BoosterData Data { get; private set; }

    public event UnityAction<BoosterMenuSlot> AddButtonClicked;
    public event UnityAction<BoosterMenuSlot, BoosterData> RemoveButtonClicked;

    private void OnEnable()
    {
        _addButton.onClick.AddListener(OnAddButtonClick);
        _removeButton.onClick.AddListener(OnRemoveButtonClick);
        LocalizationManager.LocalizationChanged += OnLocalizationChanged;

        Data = null;
        _locked = false;
        SetDefaultView();
    }

    private void OnDisable()
    {
        _addButton.onClick.RemoveListener(OnAddButtonClick);
        _removeButton.onClick.RemoveListener(OnAddButtonClick);
        LocalizationManager.LocalizationChanged -= OnLocalizationChanged;
    }

    private void OnLocalizationChanged()
    {
        if (_lockLevel != 0)
        {
            RenderLocked(_lockLevel);
            return;
        }

        if (Data == null)
            SetDefaultView();
        else
            SetData(Data);
    }

    public void SetData(BoosterData data)
    {
        Data = data;

        _preview.sprite = data.Preview;
        _name.text = LocalizationManager.Localize(data.Name);
    }

    public void RenderLocked(int lockLevel)
    {
        _locked = true;
        _lockLevel = lockLevel;

        _preview.sprite = _lockIcon;

        var localizeText = LocalizationManager.Localize("UICanvas.BoosterSlotLock", lockLevel);
        _name.text = localizeText;

        _removeButton.gameObject.SetActive(false);
    }

    private void OnAddButtonClick()
    {
        if (Data != null)
            return;

        if (_locked == false)
            AddButtonClicked?.Invoke(this);
    }

    private void OnRemoveButtonClick()
    {
        if (Data == null)
            return;

        RemoveButtonClicked?.Invoke(this, Data);

        Data = null;
        SetDefaultView();
    }

    private void SetDefaultView()
    {
        _preview.sprite = _defaultIcon;
        _name.text = LocalizationManager.Localize("Boosters.AddBooster");
    }
}
