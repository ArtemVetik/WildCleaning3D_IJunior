using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private CurrentLevelLoader _levelLoader;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _replayLevelButton;

    private void OnEnable()
    {
        _nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);
        _replayLevelButton.onClick.AddListener(OnReplayLevelButtonClicked);
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClicked);
        _replayLevelButton.onClick.RemoveListener(OnReplayLevelButtonClicked);
    }

    private void OnNextLevelButtonClicked()
    {
        _levelLoader.LoadNext();
    }

    private void OnReplayLevelButtonClicked()
    {
        _levelLoader.Reload();
    }
}
