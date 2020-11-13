using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CellObject : MonoBehaviour
{
    public GameCell CurrentCell { get; protected set; }

    public void Init(GameCell currentCell)
    {
        CurrentCell = currentCell;
    }
}
