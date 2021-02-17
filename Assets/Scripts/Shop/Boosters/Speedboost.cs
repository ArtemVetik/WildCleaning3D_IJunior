using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Speedboost", menuName = "Shop/Boosters/Speedboost", order = 51)]
public class Speedboost : Booster
{
    [SerializeField] private ParticleSystem _fireEffect;

    public override event UnityAction<Booster> Used;

    public override void Use()
    {
        Player player = FindObjectOfType<Player>();
        player.ModifiedCharacteristics(new SpeedBoostPlayer(player.PlayerData));

        Instantiate(_fireEffect, player.transform);

        Used?.Invoke(this);
    }
}
