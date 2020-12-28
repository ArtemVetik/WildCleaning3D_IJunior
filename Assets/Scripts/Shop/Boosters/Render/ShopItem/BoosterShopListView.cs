using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterShopListView : MonoBehaviour
{
    [SerializeField] private BoosterShopPresenter _template;
    [SerializeField] private Transform _container;

    public IEnumerable<BoosterShopPresenter> Render(IEnumerable<BoosterData> boostersList)
    {
        var presenters = new List<BoosterShopPresenter>();

        foreach (BoosterData data in boostersList)
        {
            var instBooster = Instantiate(_template, _container);
            instBooster.Render(data);
            presenters.Add(instBooster);
        }

        return presenters;
    }
}
