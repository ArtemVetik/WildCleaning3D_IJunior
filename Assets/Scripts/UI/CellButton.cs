using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class CellButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceText;

    private Button _button;

    public event UnityAction<CellButton> ButtonClicked;
    public event UnityAction<CellButton> Enabled;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
        Enabled?.Invoke(this);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    public void RenderPrice(int price, bool locked = false)
    {
        _priceText.text = price.ToString();

        if (locked)
            _priceText.color = Color.gray;
        else
            _priceText.color = Color.white;
    }

    private void OnButtonClicked()
    {
        ButtonClicked?.Invoke(this);
    }
}
