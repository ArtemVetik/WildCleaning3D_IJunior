using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatternMoveSystem
{
    private PlaneMoveSystem _moveSystem;
    private MovePattern _pattern;
    private MeshHeight _meshHeight;
    private int _patternIndex;

    public event UnityAction<GameCell> MoveStarted;
    public event UnityAction MovePausing;

    public PatternMoveSystem(PlaneMoveSystem moveSystem)
    {
        _moveSystem = moveSystem;
    }

    public void StartMove(GameCell fromCell, MovePattern pattern, MeshHeight meshHeight)
    {
        if (pattern == null || pattern.VectorPattern.Count == 0)
            return;

        _pattern = pattern;
        _meshHeight = meshHeight;
        _patternIndex = 0;

        MoveNext(fromCell);
    }

    private void MoveNext(GameCell from)
    {
        GameCell adjacentCell = from.TryGetAdjacent(_pattern.VectorPattern[_patternIndex]);
        if (adjacentCell == null)
        {
            _patternIndex = (_patternIndex + 1) % _pattern.VectorPattern.Count;
            _moveSystem.StartCoroutine(MoveNextWithPause(from, 1.5f));
            return;
        }

        _moveSystem.MoveEnded += OnMoveEnded;

        var nextPatternIndex = (_patternIndex + 1) % _pattern.VectorPattern.Count;
        var previousPatternIndex = (_patternIndex - 1 < 0) ? _pattern.VectorPattern.Count - 1 : _patternIndex - 1;

        var currentPattern = _pattern.VectorPattern[_patternIndex];
        var nextPattern = _pattern.VectorPattern[nextPatternIndex];
        var previousPattern = _pattern.VectorPattern[previousPatternIndex];

        var moveType = PlaneMoveSystem.MoveType.Normal;
        if (currentPattern == nextPattern && currentPattern == previousPattern)
            moveType = PlaneMoveSystem.MoveType.Normal;
        else if (previousPattern != currentPattern && currentPattern != nextPattern)
            moveType = PlaneMoveSystem.MoveType.StartEndLerp;
        else if (previousPattern != currentPattern)
            moveType = PlaneMoveSystem.MoveType.StartLerp;
        else if (currentPattern != nextPattern)
            moveType = PlaneMoveSystem.MoveType.EndLerp;

        _moveSystem.StartMove(adjacentCell, _pattern.VectorPattern[_patternIndex], _meshHeight.MaxMeshHeight, moveType);
        MoveStarted?.Invoke(adjacentCell);
    }

    private IEnumerator MoveNextWithPause(GameCell from, float pause)
    {
        MovePausing?.Invoke();
        yield return new WaitForSeconds(pause);
        MoveNext(from);
    }

    private void OnMoveEnded(GameCell finishCell)
    {
        _moveSystem.MoveEnded -= OnMoveEnded;
        _patternIndex = (_patternIndex + 1) % _pattern.VectorPattern.Count;

        MoveNext(finishCell);
    }
}
