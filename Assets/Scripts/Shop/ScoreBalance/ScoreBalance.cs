using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class ScoreBalance : ISavedObject
{
    public int Balance { get; private set; }

    public static event UnityAction<int> ScoreChanged;

    public void AddScore(int value)
    {
        Balance += value;
        ScoreChanged?.Invoke(Balance);
    }

    public bool SpendScore(int value)
    {
        if (Balance < value)
            return false;

        Balance -= value;
        ScoreChanged?.Invoke(Balance);
        return true;
    }

    public void Load(ISaveLoadVisiter saveLoadVisiter)
    {
        var saved = saveLoadVisiter.Load(this);

        Balance = saved.Balance;
    }

    public void Save(ISaveLoadVisiter saveLoadVisiter)
    {
        saveLoadVisiter.Save(this);
    }
}
