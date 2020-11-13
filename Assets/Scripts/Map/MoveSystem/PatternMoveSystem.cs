using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatternMoveSystem 
{
	private MoveSystem _moveSystem;
	private List<Vector2Int> _pattern;
	private int _patternIndex;

	public event UnityAction<GameCell> MoveStarted;

	public PatternMoveSystem(MoveSystem moveSystem)
	{
		_moveSystem = moveSystem;
	}

	public void StartMove(GameCell fromCell, List<Vector2Int> pattern)
	{
		if (pattern == null || pattern.Count == 0)
			return;

		_pattern = pattern;
		_patternIndex = 0;

		MoveNext(fromCell);
	}

	private void MoveNext(GameCell from)
	{
		GameCell adjacentCell = from.TryGetAdjacent(_pattern[_patternIndex]);
		if (adjacentCell == null)
			return;

		_moveSystem.MoveEnded += OnMoveEnded;
		_moveSystem.Move(adjacentCell, _pattern[_patternIndex]);

		MoveStarted?.Invoke(adjacentCell);
	}

	private void OnMoveEnded(GameCell finishCell)
	{
		_moveSystem.MoveEnded -= OnMoveEnded;
		_patternIndex = (_patternIndex + 1) % _pattern.Count;

		MoveNext(finishCell);
	}
}
