using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatternMoveSystem 
{
	private PlaneMoveSystem _moveSystem;
	private MovePattern _pattern;
	private int _patternIndex;

	public event UnityAction<GameCell> MoveStarted;
	public event UnityAction MovePausing;

	public PatternMoveSystem(PlaneMoveSystem moveSystem)
	{
		_moveSystem = moveSystem;
	}

	public void StartMove(GameCell fromCell, MovePattern pattern)
	{
		if (pattern == null || pattern.VectorPattern.Count == 0)
			return;

		_pattern = pattern;
		_patternIndex = 0;

		MoveNext(fromCell);
	}

	private void MoveNext(GameCell from)
	{
		GameCell adjacentCell = from.TryGetAdjacent(_pattern.VectorPattern[_patternIndex]);
		if (adjacentCell == null)
		{
			_patternIndex = (_patternIndex + 1) % _pattern.VectorPattern.Count;
			from.StartCoroutine(MoveNextWithPause(from, 1.5f));
			return;
		}

		_moveSystem.MoveEnded += OnMoveEnded;
		_moveSystem.Move(adjacentCell, _pattern.VectorPattern[_patternIndex]);

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
