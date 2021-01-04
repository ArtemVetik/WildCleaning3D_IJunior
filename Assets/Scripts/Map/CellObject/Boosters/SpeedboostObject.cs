using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedboostObject : BoosterObject
{
    public override void Triggered(CellObject cellObject)
    {
        if (cellObject is Player == false)
            return;

        BoosterInventory inventory = new BoosterInventory();
        inventory.Load(new JsonSaveLoad());

        foreach (var data in BoostersDataBase.Data)
        {
            if (data.Booster.Equals(Booster))
            {
                inventory.Add(data);
                break;
            }
        }

        inventory.Save(new JsonSaveLoad());
        Destroy(gameObject);
    }
}
