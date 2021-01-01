using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Virus : Enemy
{
    public override event UnityAction<Enemy> Died;

    public override void Die()
    {
        Died?.Invoke(this);
        Destroy(gameObject);
    }

    protected override void OnStepToMarkedCell(GameCell markedCell)
    {
        if (CurrentStage.Microbes == 0)
            Die();
        else
            markedCell.Unmark();
    }
}
