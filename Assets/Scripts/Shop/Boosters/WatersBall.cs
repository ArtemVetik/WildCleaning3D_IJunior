using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "WatersBall", menuName = "Shop/Boosters/WatersBall", order = 51)]
public class WatersBall : Booster
{
    public override event UnityAction<Booster> Used;

    public override void Use()
    {
        Used?.Invoke(this);
    }
}
