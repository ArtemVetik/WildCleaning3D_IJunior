using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoosterSelectPresenter : MonoBehaviour
{
    [SerializeField] private Image _preview;
    [SerializeField] private Text _name;
    [SerializeField] private Text _description;
    [SerializeField] private Text _count;
    [SerializeField] private Button _selectButton;

    public BoosterData Data { get; private set; }

    public event UnityAction<BoosterSelectPresenter> SelectButtonClicked;

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(OnSelectButtonClick);
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveListener(OnSelectButtonClick);
    }

    public void Render(BoosterData data, int count)
    {
        Data = data;

        _preview.sprite = data.Preview;
        _name.text = data.Name;
        _description.text = data.Description;
        _count.text = count.ToString();
    }

    private void OnSelectButtonClick()
    {
        SelectButtonClicked?.Invoke(this);
    }
}
