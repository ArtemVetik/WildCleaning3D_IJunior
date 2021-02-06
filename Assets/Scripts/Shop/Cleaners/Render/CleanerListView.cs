using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class CleanerListView : MonoBehaviour
{
    [SerializeField] private CleanerPresenter _template;
    [SerializeField] private Transform _container;

    private List<CleanerPresenter> _presenters;

    public IEnumerable<CleanerPresenter> Render(IEnumerable<CleanerData> cleaners)
    {
        _presenters = new List<CleanerPresenter>();

        foreach (var cleaner in cleaners)
        {
            var presenter = Instantiate(_template, _container);
            presenter.Render(cleaner);

            _presenters.Add(presenter);
        }

        return _presenters;
    }

    private void OnDisable()
    {
        Debug.Log("Disable list " + _presenters);
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy list " + _presenters);
    }
}
