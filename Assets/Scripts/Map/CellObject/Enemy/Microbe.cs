using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Microbe : Enemy
{
    public override event UnityAction<Enemy> Died;

    protected override void OnStepToMarkedCell(GameCell markedCell)
    {
        Died?.Invoke(this);
        Destroy(gameObject);
    }
}
