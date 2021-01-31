using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventoryListView : MonoBehaviour
{
    [SerializeField] private ChestInventoryPresenter _template;
    [SerializeField] private Transform _container;

    public IEnumerable<ChestInventoryPresenter> Render(IEnumerable<KeyValuePair<Chest, int>> boostersList)
    {
        var presenters = new List<ChestInventoryPresenter>();

        foreach (var data in boostersList)
        {
            var instBooster = Instantiate(_template, _container);
            instBooster.Render(data.Key, data.Value);
            presenters.Add(instBooster);
        }

        return presenters;
    }
}
