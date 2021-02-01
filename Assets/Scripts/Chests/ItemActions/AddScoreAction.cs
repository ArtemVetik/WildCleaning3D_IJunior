using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Add Score Action", menuName = "Chests/Actions/Add Score Action", order = 51)]
public class AddScoreAction : ChestItemAction
{
    [SerializeField] private int _scoreValue;

    public override void Use()
    {
        ScoreBalance balance = new ScoreBalance();
        balance.Load(new JsonSaveLoad());
        balance.AddScore(_scoreValue);
        balance.Save(new JsonSaveLoad());
    }
}
