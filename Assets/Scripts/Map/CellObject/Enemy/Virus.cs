using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Virus : Enemy
{
    [SerializeField] private ParticleSystem _diedEffectTemplate;

    public override event UnityAction<Enemy> Died;

    public override void Die(DeadType deadType = DeadType.Standart)
    {
        Instantiate(_diedEffectTemplate, transform.position, _diedEffectTemplate.transform.rotation);

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
