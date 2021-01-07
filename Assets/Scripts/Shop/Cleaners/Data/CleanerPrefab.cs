using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cleaner", menuName = "Shop/Cleaners/Cleaner", order = 51)]
public class CleanerPrefab : ScriptableObject
{
    [SerializeField] private Player _cleaner;

    public Player Cleaner => _cleaner;
}
