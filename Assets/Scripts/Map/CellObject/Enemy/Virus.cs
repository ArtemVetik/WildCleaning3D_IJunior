using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Virus : Enemy
{
    public override event UnityAction<Enemy> Died;

    protected override void OnStepToMarkedCell(GameCell markedCell)
    {
        if (Container.MicrobeCount == 0)
        {
            Died?.Invoke(this);
            Destroy(gameObject);
        }
        else
        {
            markedCell.Unmark();
        }
    }
}
