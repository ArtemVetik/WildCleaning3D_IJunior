using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Microbe : Enemy
{
    [SerializeField] private ParticleSystem _standartDiedEffectTemplate;
    [SerializeField] private ParticleSystem _specialDiedEffectTemplate;
    [SerializeField] private Transform _effectPosition;

    public override event UnityAction<Enemy> Died;

    public override void Die(DeadType deadType = DeadType.Standart)
    {
        if (deadType == DeadType.Standart)
            Instantiate(_standartDiedEffectTemplate, _effectPosition.position, _standartDiedEffectTemplate.transform.rotation);
        else if (deadType == DeadType.Special)
            Instantiate(_specialDiedEffectTemplate, _effectPosition.position, _specialDiedEffectTemplate.transform.rotation);

        Died?.Invoke(this);
        Destroy(gameObject);
    }

    protected override void OnStepToMarkedCell(GameCell markedCell)
    {
        Die();
    }
}
