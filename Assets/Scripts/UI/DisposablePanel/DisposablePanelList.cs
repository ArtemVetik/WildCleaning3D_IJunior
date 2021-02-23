using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposablePanelList : MonoBehaviour
{
    [SerializeField] private CurrentLevelLoader _levelLoader;

    private DisposablePanel[] _panels;

    private void Awake()
    {
        _panels = GetComponentsInChildren<DisposablePanel>(true);
    }

    private void Start()
    {
        foreach (var panel in _panels)
            if (panel.TryOpen(_levelLoader.LevelIndex + 1))
                break;
    }
}
