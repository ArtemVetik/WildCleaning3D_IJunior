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
        if (string.IsNullOrEmpty(_selectedGUID))
            SelectCleaner(_dataBase.DefaultData);
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

    public bool Contains(PlayerData playerData)
    {
        foreach (var cleanerData in _dataBase.Data)
        {
            if (cleanerData.Prefab.Cleaner.DefaultCharacteristics.ID == playerData.ID && Contains(cleanerData))
                return true;
        }

        return false;
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

        if (string.IsNullOrEmpty(_selectedGUID))
            SelectCleaner(_dataBase.DefaultData);
    }

    public void Save(ISaveLoadVisiter saveLoadVisiter)
    {
        saveLoadVisiter.Save(this);
    }
}
