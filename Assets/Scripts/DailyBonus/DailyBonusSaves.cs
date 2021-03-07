using System;
using UnityEngine;

[Serializable]
public class DailyBonusSaves : ISavedObject
{
    [SerializeField] private long _binaryTime;
    [SerializeField] private int _daysInGame;

    public int MaxDays => 4;
    public long BinaryTime => _binaryTime;
    public int DaysInGame => _daysInGame;

    public DailyBonusSaves()
    {
        _binaryTime = DateTime.Now.ToBinary();
        _daysInGame = 0;
    }

    public void AddDayInGame(DateTime time)
    {
        _daysInGame++;
        if (_daysInGame > MaxDays)
            _daysInGame = 0;

        _binaryTime = time.ToBinary();
    }

    public void ResetDaysInGame()
    {
        _daysInGame = 0;
        _binaryTime = DateTime.Now.ToBinary();
    }

    public void Load(ISaveLoadVisiter saveLoadVisiter)
    {
        var save = saveLoadVisiter.Load(this);

        _binaryTime = save.BinaryTime;
        _daysInGame = save.DaysInGame;
    }

    public void Save(ISaveLoadVisiter saveLoadVisiter)
    {
        saveLoadVisiter.Save(this);
    }
}