using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class GameScoreViewer : MonoBehaviour
{
    [SerializeField] private PlayerScore _playerScore;
    
    private TMP_Text _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();
        _scoreText.text = "0";
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
        _scoreText.text = score.ToString();
    }
}
