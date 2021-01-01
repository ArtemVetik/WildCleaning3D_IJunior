using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "WatersBall", menuName = "Shop/Boosters/WatersBall", order = 51)]
public class WatersBall : Booster
{
    [SerializeField] private CellSelector _selectorTemplate;
    [SerializeField] private int _radius;

    private CellSelector _instSelector;

    public override event UnityAction<Booster> Used;


    public override void Use()
    {
        if (_instSelector)
            return;

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
        FillInRadius(cell, cell, _radius);

        Used?.Invoke(this);
        ClearSelector();
    }

    private void FillInRadius(GameCell  currentCell, GameCell center, float radius)
    {
        if (Vector2Int.Distance(currentCell.Position, center.Position) > radius)
            return;

        currentCell.Mark();
        GameCell adjacent = null;
        if (currentCell.TryGetAdjacent(out adjacent, Vector2Int.left) && adjacent.IsMarked == false)
            FillInRadius(adjacent, center, radius);
        if (currentCell.TryGetAdjacent(out adjacent, Vector2Int.right) && adjacent.IsMarked == false)
            FillInRadius(adjacent, center, radius);
        if (currentCell.TryGetAdjacent(out adjacent, Vector2Int.up) && adjacent.IsMarked == false)
            FillInRadius(adjacent, center, radius);
        if (currentCell.TryGetAdjacent(out adjacent, Vector2Int.down) && adjacent.IsMarked == false)
            FillInRadius(adjacent, center, radius);
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
