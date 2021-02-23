using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class BoosterData : GUIDData
{
    [SerializeField] private Sprite _preview;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Booster _booster;
    [SerializeField] private int _price; 
    [SerializeField] private GameObject _emptyModel;

    public Sprite Preview => _preview;
    public string Name => _name;
    public string Description => _description;
    public Booster Booster => _booster;
    public int Price => _price;
    public GameObject EmptyModel => _emptyModel;
}
