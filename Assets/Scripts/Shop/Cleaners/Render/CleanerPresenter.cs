using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CleanerPresenter : MonoBehaviour
{
    [SerializeField] private Image _preview;
    [SerializeField] private Text _name;
    [SerializeField] private Text _description;
    [SerializeField] private Button _cellButton;
    [SerializeField] private Button _selectButton;

    public CleanerData Data { get; private set; }

    public event UnityAction<CleanerPresenter> CellButtonClicked;
    public event UnityAction<CleanerPresenter> SelectButtonClicked;

    private void OnEnable()
    {
        _cellButton.onClick.AddListener(OnCellButtonClick);
        _selectButton.onClick.AddListener(OnSelectButtonClick);
    }

    public void Render(CleanerData data)
    {
        Data = data;

        _preview.sprite = data.Preview;
        _preview.color = Color.white;
        _name.text = data.Name;
        _description.text = data.Description;

        _cellButton.gameObject.SetActive(true);
        _selectButton.gameObject.SetActive(false);
    }

    public void RenderBuyed(CleanerData data)
    {
        Data = data;

        _preview.sprite = data.Preview;
        _preview.color = Color.white;
        _name.text = data.Name;
        _description.text = data.Description;

        _cellButton.gameObject.SetActive(false);
        _selectButton.gameObject.SetActive(true);
    }

    public void RenderSelected(CleanerData data)
    {
        Data = data;

        _preview.sprite = data.Preview;
        _preview.color = Color.green;
        _name.text = data.Name;
        _description.text = data.Description;

        _cellButton.gameObject.SetActive(false);
        _selectButton.gameObject.SetActive(true);
    }

    private void OnCellButtonClick()
    {
        CellButtonClicked?.Invoke(this);
    }

    private void OnSelectButtonClick()
    {
        SelectButtonClicked?.Invoke(this);
    }

    private void OnDisable()
    {
        _cellButton.onClick.RemoveListener(OnCellButtonClick);
        _selectButton.onClick.RemoveListener(OnSelectButtonClick);
    }
}
