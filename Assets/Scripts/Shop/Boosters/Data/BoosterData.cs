using System.Collections;
using System;
using UnityEngine;

[Serializable]
public struct BoosterData
{
    [SerializeField] private Sprite _preview;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Booster _booster;

    public Sprite Preview => _preview;
    public string Name => _name;
    public string Description => _description;
    public Booster Booster => _booster;
}
