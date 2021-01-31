using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChestInventoryPresenter : MonoBehaviour
{
    [SerializeField] private Image _preview;
    [SerializeField] private Text _name;
    [SerializeField] private Text _count;
    [SerializeField] private Button _useButton;

    public Chest Data { get; private set; }

    public event UnityAction<ChestInventoryPresenter> UseButtonClicked;

    public void Render(Chest data, int count)
    {
        Data = data;

        _preview.sprite = data.Preview;
        _name.text = data.Name;

        _count.text = count.ToString();
    }

    private void OnEnable()
    {
        _useButton.onClick.AddListener(OnUseButtonClicked);
    }

    private void OnDisable()
    {
        _useButton.onClick.RemoveListener(OnUseButtonClicked);
    }

    private void OnUseButtonClicked()
    {
        UseButtonClicked?.Invoke(this);
    }
}
