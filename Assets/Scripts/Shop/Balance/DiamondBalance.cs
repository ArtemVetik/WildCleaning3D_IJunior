using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DiamondBalance : ISavedObject
{
    [SerializeField] private int _balance;

    public int Balance => _balance;

    public static event UnityAction<int> DiamondChanged;

    public void AddDiamond(int value)
    {
        _balance += value;
        DiamondChanged?.Invoke(Balance);
    }

    public bool SpendDiamond(int value)
    {
        if (Balance < value)
            return false;

        _balance -= value;
        DiamondChanged?.Invoke(Balance);
        return true;
    }

    public void Load(ISaveLoadVisiter saveLoadVisiter)
    {
        var saved = saveLoadVisiter.Load(this);

        _balance = saved.Balance;
    }

    public void Save(ISaveLoadVisiter saveLoadVisiter)
    {
        saveLoadVisiter.Save(this);
    }
}
