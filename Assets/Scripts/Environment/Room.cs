using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct LevelRange
{
    [SerializeField] private int _minimumLevel;
    [SerializeField] private int _maximumLevel;

    public int MinimumLevel => _minimumLevel;
    public int MaximumLevel => _maximumLevel;
}

public class Room : MonoBehaviour
{
    [SerializeField] private CurrentLevelLoader _levelLoader;
    [SerializeField] private Transform _roomFloorObject;

    private Decor[] _decors;
    public Bounds Bounds { get; private set; }

    private void Awake()
    {
        _decors = GetComponentsInChildren<Decor>(true);
        Bounds = new Bounds(_roomFloorObject.position, _roomFloorObject.lossyScale);
    }

    private void Start()
    {
        ShowDecor(_levelLoader.LevelIndex + 1);
    }

    private void ShowDecor(int level)
    {
        foreach (var decor in _decors)
        {
            if (decor.TargetLevel != level)
                decor.Hide();
            else
                decor.Show();
        }
    }

    public void ShowDecorInEditor(int level)
    {
        _decors = GetComponentsInChildren<Decor>(true);

        foreach (var decor in _decors)
        {
            if (decor.TargetLevel != level)
                decor.Hide();
            else
                decor.Show();
        }
    }
}
