using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentLevelLoader : MonoBehaviour
{
    [SerializeField] private LevelDataBase _levelDataBase;

    public const string PlayerPrefsKey = "CurrentLevelNumber";

    private int _currentLevelIndex;

    public LevelData CurrentLevel => _levelDataBase[_currentLevelIndex];

    private void Awake()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKey) == false)
            PlayerPrefs.SetInt(PlayerPrefsKey, 0);

        _currentLevelIndex = PlayerPrefs.GetInt(PlayerPrefsKey);
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNext()
    {
        _currentLevelIndex++;
        PlayerPrefs.SetInt(PlayerPrefsKey, _currentLevelIndex);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
