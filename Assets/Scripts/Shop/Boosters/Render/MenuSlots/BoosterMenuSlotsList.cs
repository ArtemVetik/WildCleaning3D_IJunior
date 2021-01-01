using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoosterMenuSlotsList : MonoBehaviour
{
    [SerializeField] private BoosterSelectPanel _selectPanel;
    [SerializeField] private BoosterGameSlots _gameSlots;
    [SerializeField] private BoosterMenuSlot _template;
    [SerializeField] private Transform _container;

    public readonly int MaxCount = 3;

    private List<BoosterMenuSlot> _slots;
    private BoosterMenuSlot _currentSlot;
    private BoosterInventory _inventory;
    private bool _applySaved;

    private void OnEnable()
    {
        _inventory = new BoosterInventory();
        _inventory.Load(new JsonSaveLoad());

        _applySaved = false;

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
        _inventory.Add(data);
        _inventory.Save(new JsonSaveLoad());
    }

    private void OnBoosterSelected(BoosterData data)
    {
        _currentSlot.SetData(data);
        _inventory.Remove(data);
        _inventory.Save(new JsonSaveLoad());
    }

    public void Apply()
    {
        _applySaved = true;
    }

    private void OnDisable()
    {
        List<BoosterData> boosters = new List<BoosterData>();
        foreach (var slot in _slots)
        {
            slot.AddButtonClicked -= OnSlotAddButtonClicked;
            slot.RemoveButtonClicked -= OnSlotRemoveButtonClicked;

            if (_applySaved == false && slot.Data != null)
                _inventory.Add(slot.Data.Value);
            if (slot.Data != null)
                boosters.Add(slot.Data.Value);
        }

        foreach (var slot in _slots)
            Destroy(slot.gameObject);

        _selectPanel.SelectButtonClicked -= OnBoosterSelected;

        _inventory.Save(new JsonSaveLoad());
        if (_applySaved)
        {
            _gameSlots.SetBoosters(boosters);
        }
    }
}
