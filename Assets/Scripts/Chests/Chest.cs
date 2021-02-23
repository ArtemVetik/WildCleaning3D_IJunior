using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New chest", menuName = "Chests/Add chest", order = 51)]
public class Chest : BaseScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _preview;
    [SerializeField] private ChestAnimation _chestAnimation;
    [SerializeField] private List<ChestItem> _items = new List<ChestItem>();

    public string Name => _name;
    public string Description => _description;
    public int Price => _price;
    public Sprite Preview => _preview;
    public ChestAnimation ChestAnimation => _chestAnimation;
    public IEnumerable<ChestItem> Items => _items;
}
