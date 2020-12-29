using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoosterSelectPanel : MonoBehaviour
{
    [SerializeField] private BoosterSelectListView _boosterListView;

    private IEnumerable<BoosterSelectPresenter> _presenters;

    public event UnityAction<BoosterData> SelectButtonClicked;

    public void OpenPanel(IEnumerable<BoosterData> boosters)
    {
        var groupsData = GroupBoosters(boosters);
        _presenters = _boosterListView.Render(groupsData);
        InitButtonEvents();

        gameObject.SetActive(true);
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

    private void InitButtonEvents()
    {
        foreach (var presenter in _presenters)
            presenter.SelectButtonClicked += OnSelectButtonClicked;
    } 

    private void RemoveButtonEvents()
    {
        foreach (var presenter in _presenters)
            presenter.SelectButtonClicked -= OnSelectButtonClicked;
    }

    private void OnSelectButtonClicked(BoosterSelectPresenter presenter)
    {
        SelectButtonClicked?.Invoke(presenter.Data);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        RemoveButtonEvents();
        foreach (var presenter in _presenters)
            Destroy(presenter.gameObject);

        _presenters = null;
    }
}
