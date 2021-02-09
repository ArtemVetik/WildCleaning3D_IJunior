using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlayer : MonoBehaviour
{
    [SerializeField] private PlayerInitializer _playerInitializer;
    [SerializeField] private EndLevelTrigger _endTrigger;
    
    private void OnLevelCompleted()
    {
        _playerInitializer.InstPlayer.Characteristics.Upgrade();
        _playerInitializer.InstPlayer.Characteristics.Save(new JsonSaveLoad());
    }

    private void OnEnable()
    {
        _endTrigger.LevelCompleted += OnLevelCompleted;
    }

    private void OnDestroy()
    {
        _endTrigger.LevelCompleted -= OnLevelCompleted;
    }
}
