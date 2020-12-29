using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSelectListView : MonoBehaviour
{
    [SerializeField] private BoosterSelectPresenter _template;
    [SerializeField] private Transform _container;

    public IEnumerable<BoosterSelectPresenter> Render(IEnumerable<KeyValuePair<BoosterData, int>> boostersList)
    {
        var presenters = new List<BoosterSelectPresenter>();

        foreach (var data in boostersList)
        {
            var instBooster = Instantiate(_template, _container);
            instBooster.Render(data.Key, data.Value);
            presenters.Add(instBooster);
        }

        return presenters;
    }
}
