using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameScoreViewer : MonoBehaviour
{
    [SerializeField] private PlayerScore _playerScore;
    
    private Text _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<Text>();
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
