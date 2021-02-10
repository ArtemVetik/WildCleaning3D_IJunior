﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Speedboost", menuName = "Shop/Boosters/Speedboost", order = 51)]
public class Speedboost : Booster
{
    public override event UnityAction<Booster> Used;

    public override void Use()
    {
        Player player = FindObjectOfType<Player>();
        player.ModifiedCharacteristics(new SpeedBoostPlayer(player.PlayerData));

        Used?.Invoke(this);
    }
}
