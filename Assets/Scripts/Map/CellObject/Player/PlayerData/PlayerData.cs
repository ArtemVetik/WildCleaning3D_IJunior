using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData : IPlayerData, ISavedObject
{
    [SerializeField] private float _speed;

    public PlayerData()
    {
        _speed = 2f;
    }

    public virtual float Speed => _speed;

    public void Load(ISaveLoadVisiter saveLoadVisiter)
    {
        saveLoadVisiter.Load(this);
    }

    public void Save(ISaveLoadVisiter saveLoadVisiter)
    {
        saveLoadVisiter.Save(this);
    }
}
