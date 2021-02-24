using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusListView : MonoBehaviour
{
    [SerializeField] private DailyBonusPresenter _template;
    [SerializeField] private Transform _container;

    public IEnumerable<DailyBonusPresenter> Render(IEnumerable<DailyBonusData> table)
    {
        var presenters = new List<DailyBonusPresenter>();

        foreach (var tableItem in table)
        {
            var inst = Instantiate(_template, _container);
            inst.Render(tableItem);

            presenters.Add(inst);
        }

        return presenters;
    }
}
