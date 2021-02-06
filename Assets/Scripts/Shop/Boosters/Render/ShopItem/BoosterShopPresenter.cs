using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class BoosterShopPresenter : MonoBehaviour
{
    [SerializeField] private Transform _modelContainer;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;

    private GameObject _model;

    public BoosterData Data { get; private set; }

    public UnityAction<BoosterShopPresenter> SellButtonClicked;

    public void InitButtonsEvent(Button cellButton)
    {
        cellButton.onClick.AddListener(OnCellButtonClick);
    }

    public void RemoveButtonsEvent(Button cellButton)
    {
        cellButton.onClick.RemoveListener(OnCellButtonClick);
    }

    public void Render(BoosterData data)
    {
        Data = data;

        if (_model == null)
        {
            _model = Instantiate(data.EmptyModel, _modelContainer);
            _model.transform.localPosition = Vector3.zero;
        }

        _name.text = data.Name;
        _description.text = data.Description;
    }

    public void OnCellButtonClick()
    {
        SellButtonClicked?.Invoke(this);
    }
}
