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
        Used?.Invoke(this);
    }
}
