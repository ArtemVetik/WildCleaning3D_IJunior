using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BoosterInventory : ISavedObject
{
    [SerializeField] private List<BoosterData> _buyedBoosters = new List<BoosterData>();

    public IEnumerable<BoosterData> Data => _buyedBoosters;

    public void Add(BoosterData data)
    {
        _buyedBoosters.Add(data);
    }

    public bool Remove(BoosterData data)
    {
        return _buyedBoosters.Remove(data);
    }

    public void Load(ISaveLoadVisiter saveLoadVisiter)
    {
        var saved = saveLoadVisiter.Load(this);

        _buyedBoosters = saved._buyedBoosters;
    }

    public void Save(ISaveLoadVisiter saveLoadVisiter)
    {
        saveLoadVisiter.Save(this);
    }
}
