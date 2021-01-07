using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BoosterInventory : ISavedObject
{
    [SerializeField] private List<string> _buyedGUID = new List<string>();

    private BoostersDataBase _dataBase;

    public IEnumerable<BoosterData> Data => from guid in _buyedGUID
                                            select _dataBase.Data.First((data) => data.GUID == guid);

    public BoosterInventory(BoostersDataBase dataBase)
    {
        _dataBase = dataBase;
    }

    public void Add(BoosterData data)
    {
        _buyedGUID.Add(data.GUID);
    }

    public bool Remove(BoosterData data)
    {
        return _buyedGUID.Remove(data.GUID);
    }

    public void Load(ISaveLoadVisiter saveLoadVisiter)
    {
        var saved = saveLoadVisiter.Load(this);

        _buyedGUID = saved._buyedGUID;
    }

    public void Save(ISaveLoadVisiter saveLoadVisiter)
    {
        saveLoadVisiter.Save(this);
    }
}
