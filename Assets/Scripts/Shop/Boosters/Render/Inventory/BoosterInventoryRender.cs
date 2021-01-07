using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterInventoryRender : MonoBehaviour
{
    [SerializeField] private BoostersDataBase _dataBase;
    [SerializeField] private BoosterInventoryListView _boosterListView;

    private BoosterInventory _inventory;
    private IEnumerable<BoosterInventoryPresenter> _presenters;

    private void OnEnable()
    {
        _inventory = new BoosterInventory(_dataBase);
        _inventory.Load(new JsonSaveLoad());

        var groupsData = GroupBoosters(_inventory.Data);
        _presenters = _boosterListView.Render(groupsData);
    }

    private IEnumerable<KeyValuePair<BoosterData, int>> GroupBoosters(IEnumerable<BoosterData> boosters)
    {
        var groupsData = new Dictionary<BoosterData, int>();

        foreach (var data in boosters)
        {
            if (groupsData.ContainsKey(data))
                groupsData[data]++;
            else
                groupsData.Add(data, 1);
        }

        return groupsData;
    }

    private void OnDisable()
    {
        foreach (var presenter in _presenters)
            Destroy(presenter.gameObject);
    }
}
