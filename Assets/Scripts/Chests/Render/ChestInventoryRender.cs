using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class ChestInventoryRender : MonoBehaviour
{
    [SerializeField] private ChestDataBase _dataBase;
    [SerializeField] private ChestInventoryListView _chestListView;
    [SerializeField] private GameObject _emptyPlaceholder;

    private ChestInventory _inventory;
    private IEnumerable<ChestInventoryPresenter> _presenters;

    private void OnEnable()
    {
        _inventory = new ChestInventory(_dataBase);
        _inventory.Load(new JsonSaveLoad());

        var groupsData = GroupBoosters(_inventory.Data);
        _presenters = _chestListView.Render(groupsData);

        foreach (var presenter in _presenters)
            presenter.UseButtonClicked += OnUseButtonClicked;

        _emptyPlaceholder.SetActive(_presenters.Count() == 0);
    }

    private IEnumerable<KeyValuePair<Chest, int>> GroupBoosters(IEnumerable<Chest> boosters)
    {
        var groupsData = new Dictionary<Chest, int>();

        foreach (var data in boosters)
        {
            if (groupsData.ContainsKey(data))
                groupsData[data]++;
            else
                groupsData.Add(data, 1);
        }

        return groupsData;
    }

    private void OnUseButtonClicked(ChestInventoryPresenter presenter)
    {
        OpenChestScene.Load(presenter.Data);
    }

    private void OnDisable()
    {
        foreach (var presenter in _presenters)
            presenter.UseButtonClicked -= OnUseButtonClicked;

        foreach (var presenter in _presenters)
            Destroy(presenter.gameObject);
    }
}
