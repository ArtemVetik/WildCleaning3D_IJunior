using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "WatersBall", menuName = "Shop/Boosters/WatersBall", order = 51)]
public class WatersBall : Booster
{
    [SerializeField] private ParticleSystem _useEffect;
    [SerializeField] private CellSelector _selectorTemplate;
    [SerializeField] private int _radius;

    private LevelStages _levelStages;
    private CellSelector _instSelector;

    public override event UnityAction<Booster> Used;

    public override void Use()
    {
        if (_instSelector)
            return;

        _levelStages = FindObjectOfType<LevelStages>();
        _instSelector = Instantiate(_selectorTemplate);
        _instSelector.Raycasted += OnSelectorRaycaster;
        _instSelector.Canceled += OnCancelButtonClick;
    }

    private void OnCancelButtonClick()
    {
        ClearSelector();
    }

    private void OnSelectorRaycaster(GameCell cell)
    {
        if (_levelStages.Contains(cell) == false)
            return;

        Instantiate(_useEffect, cell.transform.position, _useEffect.transform.rotation);
        FillInRadius(cell, cell, _radius, new HashSet<GameCell>());

        Used?.Invoke(this);
        ClearSelector();
    }

    private void FillInRadius(GameCell currentCell, GameCell center, float radius, HashSet<GameCell> markedCells)
    {
        if (Vector2Int.Distance(currentCell.Position, center.Position) > radius)
            return;
        if (markedCells.Contains(currentCell))
            return;

        currentCell.Mark();
        markedCells.Add(currentCell);

        GameCell adjacent = null;

        if (currentCell.TryGetAdjacent(out adjacent, Vector2Int.left) && adjacent.IsMarked == false)
            FillInRadius(adjacent, center, radius, markedCells);
        if (currentCell.TryGetAdjacent(out adjacent, Vector2Int.right) && adjacent.IsMarked == false)
            FillInRadius(adjacent, center, radius, markedCells);
        if (currentCell.TryGetAdjacent(out adjacent, Vector2Int.up) && adjacent.IsMarked == false)
            FillInRadius(adjacent, center, radius, markedCells);
        if (currentCell.TryGetAdjacent(out adjacent, Vector2Int.down) && adjacent.IsMarked == false)
            FillInRadius(adjacent, center, radius, markedCells);
    }

    private void ClearSelector()
    {
        _instSelector.Raycasted -= OnSelectorRaycaster;
        _instSelector.Canceled -= OnCancelButtonClick;
        Destroy(_instSelector.gameObject);
        _instSelector = null;
    }

    private void OnDisable()
    {
        if (_instSelector)
            _instSelector.Raycasted -= OnSelectorRaycaster;
    }
}
