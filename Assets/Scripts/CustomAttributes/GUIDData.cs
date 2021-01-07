using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class GUIDData
{
    [ObjectId]
    [SerializeField]
    private string _guid;

    public string GUID => _guid;
}
