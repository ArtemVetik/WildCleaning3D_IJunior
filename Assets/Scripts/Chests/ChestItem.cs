using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ChestItem
{
    [SerializeField] private ChestItemAction _action;
    [SerializeField, Range(0f, 1f)] private float _probability;

    public ChestItemAction Action => _action;
    public float Probability => _probability;
}
