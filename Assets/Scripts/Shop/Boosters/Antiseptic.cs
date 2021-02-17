using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Antiseptic", menuName = "Shop/Boosters/Antiseptic", order = 51)]
public class Antiseptic : Booster
{
    public override event UnityAction<Booster> Used;

    public override void Use()
    {
        var enemyContainer = FindObjectOfType<EnemyContainer>();

        List<Enemy> microbes = new List<Enemy>();
        foreach (var enemy in enemyContainer.Enemies)
            if (enemy is Microbe)
                microbes.Add(enemy);

        for (int i = 0; i < microbes.Count; i++)
            microbes[i].Die(Enemy.DeadType.Special);

        Used?.Invoke(this);
    }
}
