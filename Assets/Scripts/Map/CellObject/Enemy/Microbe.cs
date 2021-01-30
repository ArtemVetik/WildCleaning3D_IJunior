using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Microbe : Enemy
{
    [SerializeField] private ParticleSystem _diedEffectTemplate;

    public override event UnityAction<Enemy> Died;

    public override void Die()
    {
        Instantiate(_diedEffectTemplate, transform.position, _diedEffectTemplate.transform.rotation);

        Died?.Invoke(this);
        Destroy(gameObject);
    }

    protected override void OnStepToMarkedCell(GameCell markedCell)
    {
        Die();
    }
}
