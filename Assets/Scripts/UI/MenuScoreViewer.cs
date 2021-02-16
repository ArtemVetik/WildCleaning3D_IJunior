using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScoreViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;

    private void OnEnable()
    {
        ScoreBalance.ScoreChanged += OnScoreBalanceChanged;
    }

    private void OnDisable()
    {
        ScoreBalance.ScoreChanged -= OnScoreBalanceChanged;
    }

    private void Start()
    {
        ScoreBalance balance = new ScoreBalance();
        balance.Load(new JsonSaveLoad());
        _score.text = balance.Balance.ToString();
    }

    private void OnScoreBalanceChanged(int scoreValue)
    {
        _score.text = scoreValue.ToString();
    }
}
