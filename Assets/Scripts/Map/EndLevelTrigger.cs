using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelStages _stages;
    [SerializeField] private GameCanvas _gameCanvas;
    [SerializeField] private EndOfGameCanvas _endOfGameCanvas;

    private void OnEnable()
    {
        _stages.AllStageCompeted += OnAllStagesComplete;
    }

    private void OnDisable()
    {
        _stages.AllStageCompeted -= OnAllStagesComplete;
    }

    private void OnAllStagesComplete()
    {
        Debug.Log("Level complete");
        _gameCanvas.Hide();
        _endOfGameCanvas.Show();
    }
}
