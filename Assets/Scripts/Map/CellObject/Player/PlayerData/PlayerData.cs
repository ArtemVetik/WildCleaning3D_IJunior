using System;
using UnityEngine;

[Serializable]
public class PlayerData : IPlayerData, ISavedObject
{
    [SerializeField] private float _speed;
    [SerializeField, ReadOnly] private string _id;

    public string ID => _id;

    public virtual float Speed => _speed;

    public void Load(ISaveLoadVisiter saveLoadVisiter)
    {
        PlayerData savedData = saveLoadVisiter.Load(this);
        if (savedData == null)
            return;

        _speed = savedData._speed;
    }

    public void Save(ISaveLoadVisiter saveLoadVisiter)
    {
        saveLoadVisiter.Save(this);
    }
}
