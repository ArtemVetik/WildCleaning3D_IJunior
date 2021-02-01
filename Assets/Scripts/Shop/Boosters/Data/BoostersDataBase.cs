using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Booster data base", menuName = "Shop/Boosters/BoosterDataBase", order = 51)]
public class BoostersDataBase : ScriptableObject
{
    [SerializeField] private List<BoosterData> _dataBase = new List<BoosterData>();

    public IEnumerable<BoosterData> Data => _dataBase;

    public void Add(BoosterData data)
    {
        _dataBase.Add(data);
    }

    public void RemoveAt(int index)
    {
        _dataBase.RemoveAt(index);
    }

    public BoosterData FindFirst(Booster booster)
    {
        foreach (var item in _dataBase)
            if (item.Booster.Equals(booster))
                return item;

        return null;
    }
}
