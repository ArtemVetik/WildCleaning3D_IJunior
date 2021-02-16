using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterContainer : MonoBehaviour
{
    [SerializeField] private LevelSpawner _spawner;

    private List<BoosterObject> _boosters;
    private List<BoosterObject> _collectedBoosters;

    public IEnumerable<BoosterObject> CollectedBoosters => _collectedBoosters;

    private void OnEnable()
    {
        _boosters = new List<BoosterObject>();
        _collectedBoosters = new List<BoosterObject>();

        _spawner.CellObjectSpawned += OnCellObjectSpawn;
    }

    private void OnCellObjectSpawn(CellObject cellObject)
    {
        if (cellObject is BoosterObject)
        {
            var booster = cellObject as BoosterObject;
            booster.Collected += OnBoosterCollected;
            _boosters.Add(booster);
        }
    }

    private void OnBoosterCollected(BoosterObject booster)
    {
        _collectedBoosters.Add(booster);
    }

    private void OnDisable()
    {
        _spawner.CellObjectSpawned -= OnCellObjectSpawn;

        foreach (var booster in _boosters)
            booster.Collected -= OnBoosterCollected;
    }
}
