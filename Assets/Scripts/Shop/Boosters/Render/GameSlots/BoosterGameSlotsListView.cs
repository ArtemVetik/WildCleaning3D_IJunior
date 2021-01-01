using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterGameSlotsListView : MonoBehaviour
{
    [SerializeField] private BoosterGameSlotPresenter _template;
    [SerializeField] private Transform _container;

    public IEnumerable<BoosterGameSlotPresenter> Render(IEnumerable<BoosterData> boostersList)
    {
        var presenters = new List<BoosterGameSlotPresenter>();

        foreach (var data in boostersList)
        {
            var instBooster = Instantiate(_template, _container);
            instBooster.Render(data);
            presenters.Add(instBooster);
        }

        return presenters;
    }
}
