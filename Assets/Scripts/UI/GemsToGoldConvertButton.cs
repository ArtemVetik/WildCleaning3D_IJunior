using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GemsToGoldConvertButton : MonoBehaviour
{
    [SerializeField] private int _gemsToRemove;
    [SerializeField] private int _goldToAdd;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        DiamondBalance diamond = new DiamondBalance();
        diamond.Load(new JsonSaveLoad());

        if (diamond.Balance < _gemsToRemove)
            return;

        ScoreBalance gold = new ScoreBalance();
        gold.Load(new JsonSaveLoad());

        diamond.SpendDiamond(_gemsToRemove);
        gold.AddScore(_goldToAdd);

        diamond.Save(new JsonSaveLoad());
        gold.Save(new JsonSaveLoad());
    }
}
