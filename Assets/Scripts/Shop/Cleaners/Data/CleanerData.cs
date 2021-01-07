using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class CleanerData : GUIDData
{
    [SerializeField] private Sprite _preview;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private CleanerPrefab _prefab;

    public Sprite Preview => _preview;
    public string Name => _name;
    public string Description => _description;
    public CleanerPrefab Prefab => _prefab;
}
