using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Speedboost", menuName = "Shop/Boosters/Speedboost", order = 51)]
public class Speedboost : Booster
{
    public override event UnityAction<Booster> Used;

    public override void Use()
    {
        Used?.Invoke(this);
    }
}
