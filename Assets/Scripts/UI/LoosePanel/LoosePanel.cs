using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoosePanel : MonoBehaviour
{
    [SerializeField] private CurrentLevelLoader _levelLoader;
    [SerializeField] private Button _replayLevelButton;

    private void OnEnable()
    {
        _replayLevelButton.onClick.AddListener(OnReplayLevelButtonClicked);
    }

    private void OnDisable()
    {
        _replayLevelButton.onClick.RemoveListener(OnReplayLevelButtonClicked);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnReplayLevelButtonClicked()
    {
        _levelLoader.Reload();
    }
}
