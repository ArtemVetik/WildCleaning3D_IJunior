using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimationSwitcher : MonoBehaviour
{
    [SerializeField] private CameraAnimations _animations;
    [SerializeField] private EndLevelTrigger _endTrigger;
    [SerializeField] private MapFiller _mapFiller;

    private void OnEnable()
    {
        _endTrigger.LevelFailed += OnLevelFailed;
        _endTrigger.LevelCompleted += OnLevelCompleted;
        _mapFiller.StartFilling += OnStartFilling;
        _mapFiller.EndFilled += OnEndFilling;
    }

    private void OnDisable()
    {
        _endTrigger.LevelFailed -= OnLevelFailed;
        _endTrigger.LevelCompleted -= OnLevelCompleted;
        _mapFiller.StartFilling -= OnStartFilling;
        _mapFiller.EndFilled -= OnEndFilling;
    }

    private void OnLevelFailed()
    {
        _animations.SetTrigger(CameraAnimations.Parameters.DeadShake);
    }

    private void OnLevelCompleted()
    {
        _animations.SetTrigger(CameraAnimations.Parameters.CompleteLevelLoop);
    }

    private void OnStartFilling(FillData data)
    {
        _animations.SetTrigger(CameraAnimations.Parameters.StartFilling);
    }

    private void OnEndFilling(FillData data)
    {
        _animations.SetTrigger(CameraAnimations.Parameters.EndFilling);
    }
}
