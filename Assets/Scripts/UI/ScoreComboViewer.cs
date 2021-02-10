using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Animator))]
public class ScoreComboViewer : MonoBehaviour
{
    class AnimatorParameters
    {
        public static readonly string Show = nameof(Show);
    }

    [SerializeField] private PlayerScore _playerScore;

    private Text _scoreText;
    private Animator _animator;

    private void Awake()
    {
        _scoreText = GetComponent<Text>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerScore.ScoreCombined += OnScoreCombined;
    }

    private void OnDisable()
    {
        _playerScore.ScoreCombined -= OnScoreCombined;
    }

    private void OnScoreCombined(int combo)
    {
        _scoreText.text = $"X{combo}";
        _animator.SetTrigger(AnimatorParameters.Show);
    }
}
