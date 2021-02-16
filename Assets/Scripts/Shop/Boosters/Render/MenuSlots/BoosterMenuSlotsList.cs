using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoosterMenuSlotsList : MonoBehaviour
{
    [SerializeField] private BoostersDataBase _dataBase;
    [SerializeField] private StartLevelTrigger _startTrigger;
    [SerializeField] private BoosterSelectPanel _selectPanel;
    [SerializeField] private BoosterGameSlots _gameSlots;
    [SerializeField] private BoosterMenuSlot _template;
    [SerializeField] private Transform _container;

    public readonly int MaxCount = 2;

    private List<BoosterMenuSlot> _slots;
    private BoosterMenuSlot _currentSlot;
    private BoosterInventory _inventory;
    private bool _gameStarted;

    private void OnEnable()
    {
        _inventory = new BoosterInventory(_dataBase);

        _slots = new List<BoosterMenuSlot>();
        _currentSlot = null;
        _gameStarted = false;

        for (int i = 0; i < MaxCount; i++)
        {
            var slot = Instantiate(_template, _container);
            slot.AddButtonClicked += OnSlotAddButtonClicked;
            slot.RemoveButtonClicked += OnSlotRemoveButtonClicked;
            _slots.Add(slot);
        }

        _selectPanel.SelectButtonClicked += OnBoosterSelected;
        _startTrigger.GameStarting += OnGameStarting;
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
        _inventory.Load(new JsonSaveLoad());
        foreach (var slot in _slots)
        {
            slot.AddButtonClicked -= OnSlotAddButtonClicked;
            slot.RemoveButtonClicked -= OnSlotRemoveButtonClicked;

            if (_gameStarted == false && slot.Data != null)
                _inventory.Add(slot.Data);

            Destroy(slot.gameObject);
        }

        _selectPanel.SelectButtonClicked -= OnBoosterSelected;
        _startTrigger.GameStarting -= OnGameStarting;
        _inventory.Save(new JsonSaveLoad());
    }

    private void OnGameStarting()
    {
        List<BoosterData> boosters = new List<BoosterData>();
        foreach (var slot in _slots)
        {
            if (slot.Data != null)
                boosters.Add(slot.Data);
        }

        _gameSlots.SetBoosters(boosters);
        _gameStarted = true;
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
