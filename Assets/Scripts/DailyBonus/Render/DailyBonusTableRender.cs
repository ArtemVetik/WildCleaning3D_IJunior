using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusTableRender : MonoBehaviour
{
    [SerializeField] private DailyBonusTable _table;
    [SerializeField] private DailyBonusListView _listView;

    private IEnumerable<DailyBonusPresenter> _presenters;

    private void Start()
    {
        _presenters = _listView.Render(_table.Data);
    }
}
