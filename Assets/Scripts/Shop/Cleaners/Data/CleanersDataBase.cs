using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cleaner data base", menuName = "Shop/Cleaners/CleanersDataBase", order = 51)]
public class CleanersDataBase : ScriptableObject
{
    [SerializeField] private int _defaultCleanerIndex;
    [SerializeField] private List<CleanerData> _cleaners = new List<CleanerData>();

    public IEnumerable<CleanerData> Data => _cleaners;
    public CleanerData DefaultData => _cleaners[_defaultCleanerIndex];

    public void Add(CleanerData data)
    {
        _cleaners.Add(data);
    }

    public void RemoveAt(int index)
    {
        _cleaners.RemoveAt(index);
    }
}
