using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostersCharger : MonoBehaviour
{
    [SerializeField] private BoosterContainer _container;
    [SerializeField] private EndLevelTrigger _endTrigger;
    
    private void OnEnable()
    {
        _endTrigger.LevelCompleted += OnLevelCompleted;
    }

    private void OnLevelCompleted()
    {
        foreach (var booster in _container.CollectedBoosters)
            booster.AddInInventory();
    }

    private void OnDisable()
    {
        _endTrigger.LevelCompleted -= OnLevelCompleted;
    }
}
