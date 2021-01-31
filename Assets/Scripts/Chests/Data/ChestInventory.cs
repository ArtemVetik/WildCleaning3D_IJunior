using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class ChestInventory : ISavedObject
{
    [SerializeField] private List<string> _buyedGUID = new List<string>();

    private ChestDataBase _dataBase;

    public IEnumerable<Chest> Data => from guid in _buyedGUID
                                            select _dataBase.Data.First((data) => data.GUID == guid);

    public ChestInventory(ChestDataBase dataBase)
    {
        _dataBase = dataBase;
    }

    public void Add(Chest data)
    {
        _buyedGUID.Add(data.GUID);
    }

    public bool Remove(Chest data)
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
