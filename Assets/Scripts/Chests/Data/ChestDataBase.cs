using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chest data base", menuName = "Chests/DataBase", order = 51)]
public class ChestDataBase : ScriptableObject
{
    [SerializeField] private List<Chest> _dataBase = new List<Chest>();

    public IEnumerable<Chest> Data => _dataBase;

    public void Add(Chest data)
    {
        _dataBase.Add(data);
    }

    public void RemoveAt(int index)
    {
        _dataBase.RemoveAt(index);
    }
}
