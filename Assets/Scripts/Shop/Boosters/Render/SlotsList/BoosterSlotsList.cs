using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSlotsList : MonoBehaviour
{
    [SerializeField] private BoosterSelectPanel _selectPanel;
    [SerializeField] private BoosterSlot _template;
    [SerializeField] private Transform _container;

    public readonly int MaxCount = 3;

    private List<BoosterSlot> _slots;
    private BoosterSlot _currentSlot;
    private BoosterInventory _inventory;
    private bool _applySaved;

    private void OnEnable()
    {
        _inventory = new BoosterInventory();
        _inventory.Load(new JsonSaveLoad());
        _applySaved = false;

        _slots = new List<BoosterSlot>();
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

    private void OnSlotAddButtonClicked(BoosterSlot slot)
    {
        _currentSlot = slot;
        _inventory.Load(new JsonSaveLoad());
        _selectPanel.OpenPanel(_inventory.Data);
    }

    private void OnSlotRemoveButtonClicked(BoosterSlot slot, BoosterData data)
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
        foreach (var slot in _slots)
        {
            slot.AddButtonClicked -= OnSlotAddButtonClicked;
            slot.RemoveButtonClicked -= OnSlotRemoveButtonClicked;
            if (_applySaved == false && slot.Data != null)
                _inventory.Add(slot.Data.Value);
        }

        foreach (var slot in _slots)
            Destroy(slot.gameObject);

        _selectPanel.SelectButtonClicked -= OnBoosterSelected;

        if (_applySaved)
            _inventory.Save(new JsonSaveLoad());
    }
}
