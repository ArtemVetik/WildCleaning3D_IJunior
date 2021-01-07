using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CleanerInventory : ISavedObject
{
    [SerializeField] private List<CleanerData> _buyedCleaners = new List<CleanerData>();
    [SerializeField] private CleanerData _selectedCleaner;

    public IEnumerable<CleanerData> Data => _buyedCleaners;
    public CleanerData SelectedCleaner => _selectedCleaner;

    public void Add(CleanerData data)
    {
        _buyedCleaners.Add(data);
    }

    public bool Remove(CleanerData data)
    {
        return _buyedCleaners.Remove(data);
    }

    public bool Contains(CleanerData data)
    {
        return _buyedCleaners.Contains(data);
    }

    public void SelectCleaner(CleanerData data)
    {
        _selectedCleaner = data;
    }

    public void Load(ISaveLoadVisiter saveLoadVisiter)
    {
        var saved = saveLoadVisiter.Load(this);

        _buyedCleaners = saved._buyedCleaners;
        _selectedCleaner = saved._selectedCleaner;
    }

    public void Save(ISaveLoadVisiter saveLoadVisiter)
    {
        saveLoadVisiter.Save(this);
    }
}
