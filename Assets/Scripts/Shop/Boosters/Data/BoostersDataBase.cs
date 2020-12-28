using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Booster data base", menuName = "Shop/Boosters/BoosterDataBase", order = 51)]
public class BoostersDataBase : ScriptableObject
{
    [SerializeField] private List<BoosterData> _dataBase = new List<BoosterData>();

    public IEnumerable<BoosterData> Data => _dataBase;
}
