using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntisepticObject : BoosterObject
{
    public override void Triggered(CellObject cellObject)
    {
        Debug.Log("AntisepticObject triggered");
    }
}
