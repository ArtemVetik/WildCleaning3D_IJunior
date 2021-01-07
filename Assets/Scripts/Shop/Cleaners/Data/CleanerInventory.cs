using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CleanerInventory : ISavedObject
{
    [SerializeField] private List<string> _buyedGUID = new List<string>();
    [SerializeField] private string _selectedGUID;

    private CleanersDataBase _dataBase;
    
    public IEnumerable<CleanerData> Data => from data in _dataBase.Data
                                            where _buyedGUID.Contains(data.GUID)
                                            select data;
    public CleanerData SelectedCleaner => _dataBase.Data.First((data) => data.GUID == _selectedGUID);

    public CleanerInventory(CleanersDataBase dataBase)
    {
        _dataBase = dataBase;
    }

    public void Add(CleanerData data)
    {
        _buyedGUID.Add(data.GUID);
    }

    public bool Remove(CleanerData data)
    {
        return _buyedGUID.Remove(data.GUID);
    }

    public bool Contains(CleanerData data)
    {
        return _buyedGUID.Contains(data.GUID);
    }

    public void SelectCleaner(CleanerData data)
    {
        _selectedGUID = data.GUID;
    }

    public void Load(ISaveLoadVisiter saveLoadVisiter)
    {
        var saved = saveLoadVisiter.Load(this);

        _buyedGUID = saved._buyedGUID;
        _selectedGUID = saved._selectedGUID;
    }

    public void Save(ISaveLoadVisiter saveLoadVisiter)
    {
        saveLoadVisiter.Save(this);
    }
}
