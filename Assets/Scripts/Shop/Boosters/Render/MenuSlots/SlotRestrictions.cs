using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class SlotData
{
    [SerializeField] private int _openedLevel;

    public int OpenedLevel => _openedLevel;
}

[CreateAssetMenu(fileName = "New slot restrictions", menuName = "SlotRestrictions/NewSlotsRestricion", order = 51)]
public class SlotRestrictions : ScriptableObject
{
    [SerializeField] private List<SlotData> _slots;

    public IEnumerable<SlotData> Slots => _slots;
    public int SlotCount => _slots.Count;
}
