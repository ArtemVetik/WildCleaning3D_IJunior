using System.Collections;
using System.Collections.Generic;
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
        _diamond.text = balance.Balance.ToString();
    }

    private void OnDiamondBalanceChanged(int scoreValue)
    {
        _diamond.text = scoreValue.ToString();
    }
}
