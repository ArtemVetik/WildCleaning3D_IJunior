using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileSelector : MonoBehaviour
{
    [SerializeField] private GameCell _cell;
    [SerializeField, Header("Tiles")] private GameObject _zeroBorderTile;
    [SerializeField] private GameObject _oneBorderTile;
    [SerializeField] private GameObject _twoBorderTile;
    [SerializeField] private GameObject _cornerTile;

    private List<GameCell> _adjacentCells;
    private int _emptyAdjacentCount;

    private void Start()
    {
        _adjacentCells = new List<GameCell>();

        _adjacentCells.Add(_cell.TryGetAdjacent(Vector2Int.left));
        _adjacentCells.Add(_cell.TryGetAdjacent(Vector2Int.up));
        _adjacentCells.Add(_cell.TryGetAdjacent(Vector2Int.right));
        _adjacentCells.Add(_cell.TryGetAdjacent(Vector2Int.down));

        _emptyAdjacentCount = _adjacentCells.Count(cell => cell == null);

        if (_emptyAdjacentCount == 0)
        {
            if (InitCornerTile() == false)
                InitZeroBorderTile();
        }
        else if (_emptyAdjacentCount == 1)
            InitOneBorderTile();
        else if (_emptyAdjacentCount == 2)
            InitTwoBorderTile();
    }

    private void InitZeroBorderTile()
    {
        _zeroBorderTile.SetActive(true);
    }

    private void InitOneBorderTile()
    {
        _oneBorderTile.SetActive(true);

        int angle = 0;
        for (int i = 0; i < 4; i++)
        {
            if (_adjacentCells[i] == null)
                break;

            angle += 90;
        }

        var rotation = _oneBorderTile.transform.eulerAngles;
        _oneBorderTile.transform.localRotation = Quaternion.Euler(rotation.x, angle, rotation.z);
    }


    private void InitTwoBorderTile()
    {
        _twoBorderTile.SetActive(true);

        int angle = 90;
        for (int i = 0; i < 4; i++)
        {
            if (_adjacentCells[i] == null && _adjacentCells[(i + 1) % 4] == null)
                break;

            angle += 90;
        }

        var rotation = _oneBorderTile.transform.eulerAngles;
        _twoBorderTile.transform.localRotation = Quaternion.Euler(rotation.x, angle, rotation.z);
    }

    private bool InitCornerTile()
    {
        Vector2Int direction = Vector2Int.down;
        int angle = 0;
        for (int i = 0; i < 4; i++)
        {
            if (_adjacentCells[i].TryGetAdjacent(direction) == null)
            {
                var rotation = _cornerTile.transform.eulerAngles;
                _cornerTile.transform.localRotation = Quaternion.Euler(rotation.x, angle, rotation.z);
                _cornerTile.SetActive(true);
                return true;
            }
            angle += 90;
            direction = direction.Rotate(-90);
        }
        return false;
    }
}
