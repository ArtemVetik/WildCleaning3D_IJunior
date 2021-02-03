using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanerListView : MonoBehaviour
{
    [SerializeField] private CleanerPresenter _template;
    [SerializeField] private Transform _container;

    public IEnumerable<CleanerPresenter> Render(IEnumerable<CleanerData> cleaners)
    {
        List<CleanerPresenter> presenters = new List<CleanerPresenter>();

        foreach (var cleaner in cleaners)
        {
            var presenter = Instantiate(_template, _container);
            presenter.Render(cleaner);

            presenters.Add(presenter);
        }

        return presenters;
    }
}
