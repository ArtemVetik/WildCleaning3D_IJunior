﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New chest", menuName = "Chests/Add chest", order = 51)]
public class Chest : BaseScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _preview;
    [SerializeField] private List<ChestItem> _items = new List<ChestItem>();

    public string Name => _name;
    public string Description => _description;
    public Sprite Preview => _preview;
    public IEnumerable<ChestItem> Items => _items;
}
