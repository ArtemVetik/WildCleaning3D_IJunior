using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

[CreateAssetMenu(fileName = "New Player Characteristic", menuName = "Player/Characteristic", order = 51)]
public class PlayerCharacteristics : BaseScriptableObject
{
    [SerializeField] private PlayerData _characteristic;

    private void OnValidate()
    {
        if (_characteristic != null)
        {
            var id = _characteristic.GetType().GetField("_id", BindingFlags.NonPublic | BindingFlags.Instance);
            id.SetValue(_characteristic, GUID);
        }
    }

    public PlayerData Characteristic => _characteristic;
}
