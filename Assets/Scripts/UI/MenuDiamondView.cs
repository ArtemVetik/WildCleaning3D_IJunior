using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class MenuDiamondView : MonoBehaviour
{
    [SerializeField] private TMP_Text _diamond;

    private void OnEnable()
    {
        DiamondBalance.DiamondChanged += OnDiamondBalanceChanged;
    }

    private void OnDisable()
    {
        DiamondBalance.DiamondChanged -= OnDiamondBalanceChanged;
    }

    private void Start()
    {
        DiamondBalance balance = new DiamondBalance();
        balance.Load(new JsonSaveLoad());
        SetDiamondText(balance.Balance);
    }

    private void OnDiamondBalanceChanged(int scoreValue)
    {
        SetDiamondText(scoreValue);
    }

    private void SetDiamondText(int scoreValue)
    {
        if (scoreValue == 0)
            _diamond.text = "0";
        else
            _diamond.text = string.Format(CultureInfo.InvariantCulture, "{0:#,#}", scoreValue);
    }
}
