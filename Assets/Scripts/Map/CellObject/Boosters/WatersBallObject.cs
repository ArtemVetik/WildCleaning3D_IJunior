using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatersBallObject : BoosterObject
{
    public override void Triggered(CellObject cellObject)
    {
        Debug.Log("WatersBallObject triggered");
    }
}
