using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedboostObject : BoosterObject
{
    public override void Triggered(CellObject cellObject)
    {
        Debug.Log("SpeedboostObject triggered");
    }
}
