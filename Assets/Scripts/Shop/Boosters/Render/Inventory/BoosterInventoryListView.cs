using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterInventoryListView : MonoBehaviour
{
    [SerializeField] private BoosterInventoryPresenter _template;
    [SerializeField] private Transform _container;

    public IEnumerable<BoosterInventoryPresenter> Render(IEnumerable<KeyValuePair<BoosterData, int>> boostersList)
    {
        var presenters = new List<BoosterInventoryPresenter>();

        foreach (var data in boostersList)
        {
            var instBooster = Instantiate(_template, _container);
            instBooster.Render(data.Key, data.Value);
            presenters.Add(instBooster);
        }

        return presenters;
    }
}
