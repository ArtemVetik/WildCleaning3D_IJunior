using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Daily Bonus Table", menuName = "DailyBonus/NewTable", order = 51)]
public class DailyBonusTable : ScriptableObject
{
    [SerializeField] private List<DailyBonusData> _table;

    public int ItemCount => _table.Count;

    public IEnumerable<DailyBonusData> Data => _table;

    public void RemoveAt(int index)
    {
        _table.RemoveAt(index);
    }

    public void Add(DailyBonusData data)
    {
        _table.Add(data);
    }
}
