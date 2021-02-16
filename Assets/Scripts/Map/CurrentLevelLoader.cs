using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentLevelLoader : MonoBehaviour
{
    public const string LevelNumberKey = "CurrentLevelNumber";
    public const string CompleteLevelKey = "CurrentLevelComplete";

    [SerializeField] private LevelDataBase _levelDataBase;
    [SerializeField] private EndLevelTrigger _endTrigger;

    private int _lastCompletedIndex;

    public LevelData CurrentLevel => _levelDataBase[LevelIndex];
    public int LevelIndex { get; private set; }
    public bool Completed => _lastCompletedIndex == LevelIndex;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(LevelNumberKey) == false)
            PlayerPrefs.SetInt(LevelNumberKey, 0);

        if (PlayerPrefs.HasKey(CompleteLevelKey) == false)
            PlayerPrefs.SetInt(CompleteLevelKey, 0);

        LevelIndex = PlayerPrefs.GetInt(LevelNumberKey);
        _lastCompletedIndex = PlayerPrefs.GetInt(CompleteLevelKey);
    }

    public void Reload()
    {
        PlayerPrefs.SetInt(LevelNumberKey, LevelIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnLevelCompleted()
    {
        _lastCompletedIndex = LevelIndex;
        PlayerPrefs.SetInt(CompleteLevelKey, _lastCompletedIndex);

        if (LevelIndex + 1 < _levelDataBase.Count)
            PlayerPrefs.SetInt(LevelNumberKey, LevelIndex + 1);
    }

    private void OnEnable()
    {
        _endTrigger.LevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        _endTrigger.LevelCompleted -= OnLevelCompleted;
    }
}
