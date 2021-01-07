using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoosterMenuSlotsList : MonoBehaviour
{
    [SerializeField] private BoostersDataBase _dataBase;
    [SerializeField] private BoosterSelectPanel _selectPanel;
    [SerializeField] private BoosterGameSlots _gameSlots;
    [SerializeField] private BoosterMenuSlot _template;
    [SerializeField] private Transform _container;

    public readonly int MaxCount = 3;

    private List<BoosterMenuSlot> _slots;
    private BoosterMenuSlot _currentSlot;
    private BoosterInventory _inventory;

    private void OnEnable()
    {
        _inventory = new BoosterInventory(_dataBase);

        _slots = new List<BoosterMenuSlot>();
        _currentSlot = null;

        for (int i = 0; i < MaxCount; i++)
        {
            var slot = Instantiate(_template, _container);
            slot.AddButtonClicked += OnSlotAddButtonClicked;
            slot.RemoveButtonClicked += OnSlotRemoveButtonClicked;
            _slots.Add(slot);
        }

        _selectPanel.SelectButtonClicked += OnBoosterSelected;
    }

    private void OnSlotAddButtonClicked(BoosterMenuSlot slot)
    {
        _currentSlot = slot;
        _inventory.Load(new JsonSaveLoad());
        _selectPanel.OpenPanel(_inventory.Data);
    }

    private void OnSlotRemoveButtonClicked(BoosterMenuSlot slot, BoosterData data)
    {
        _inventory.Load(new JsonSaveLoad());
        _inventory.Add(data);
        _inventory.Save(new JsonSaveLoad());
    }

    private void OnBoosterSelected(BoosterData data)
    {
        _currentSlot.SetData(data);
        _inventory.Remove(data);
        _inventory.Save(new JsonSaveLoad());
    }

    private void OnDisable()
    {
        List<BoosterData> boosters = new List<BoosterData>();
        foreach (var slot in _slots)
        {
            slot.AddButtonClicked -= OnSlotAddButtonClicked;
            slot.RemoveButtonClicked -= OnSlotRemoveButtonClicked;

            if (slot.Data != null)
                boosters.Add(slot.Data);

            Destroy(slot.gameObject);
        }

        _selectPanel.SelectButtonClicked -= OnBoosterSelected;

        _gameSlots.SetBoosters(boosters);
    }

    private void OnApplicationQuit()
    {
        _inventory.Load(new JsonSaveLoad());
        foreach (var slot in _slots)
            if (slot.Data != null)
                _inventory.Add(slot.Data);

        _inventory.Save(new JsonSaveLoad());
    }
}
