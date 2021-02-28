﻿using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class GameScoreViewer : MonoBehaviour
{
    [SerializeField] private PlayerScore _playerScore;

    private TMP_Text _scoreText;
    private Coroutine _scoreChangeCoroutine;
    private int _currentScore;

    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();

        _currentScore = 0;
        _scoreText.text = _currentScore.ToString();
    }

    private void OnEnable()
    {
        _playerScore.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _playerScore.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        if (_scoreChangeCoroutine != null)
            StopCoroutine(_scoreChangeCoroutine);

        _scoreChangeCoroutine = StartCoroutine(ScoreChanger(score));
    }

    private IEnumerator ScoreChanger(int toScore)
    {
        var delay = new WaitForSeconds(1f / (toScore - _currentScore));
        _scoreText.text = _currentScore.ToString();

        while (_currentScore < toScore)
        {
            yield return delay;
            _currentScore++;
            _scoreText.text = _currentScore.ToString();
        }
    }
}
