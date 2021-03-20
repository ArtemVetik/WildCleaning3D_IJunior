using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScoreViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;

    private void OnEnable()
    {
        GoldBalance.ScoreChanged += OnScoreBalanceChanged;
    }

    private void OnDisable()
    {
        GoldBalance.ScoreChanged -= OnScoreBalanceChanged;
    }

    private void Start()
    {
        GoldBalance balance = new GoldBalance();
        balance.Load(new JsonSaveLoad());
        SetScoreText(balance.Balance);
    }

    private void OnScoreBalanceChanged(int scoreValue)
    {
        SetScoreText(scoreValue);
    }

    private void SetScoreText(int scoreValue)
    {
        if (scoreValue == 0)
            _score.text = "0";
        else
            _score.text = scoreValue.ToString("#,#", CultureInfo.InvariantCulture);
    }
}
