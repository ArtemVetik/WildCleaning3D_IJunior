using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusTableRender : MonoBehaviour
{
    [SerializeField] private DailyBonusTable _table;
    [SerializeField] private DailyBonusListView _listView;
    [SerializeField] private TodayRewardPanel _todayRewardPanel;

    private IEnumerable<DailyBonusPresenter> _presenters;
    private DailyBonusPresenter _todayReward;
    private DailyBonusPresenter _tomorrowReward;

    private void OnEnable()
    {
        _todayRewardPanel.Closed += OnTodayRewardPanelClosed;
    }

    private void OnDisable()
    {
        _todayRewardPanel.Closed -= OnTodayRewardPanelClosed;
    }

    private void OnTodayRewardPanelClosed()
    {
        _todayReward.SetClearAnimation();

        if (_tomorrowReward != null)
            _tomorrowReward.SetFocusAnimation();
    }

    private void Start()
    {
        _presenters = _listView.Render(_table.Data);

        DailyBonusSaves saves = new DailyBonusSaves();
        saves.Load(new JsonSaveLoad());

        _todayReward = _tomorrowReward = null;
        int day = 1;

        foreach (var presenter in _presenters)
        {
            if (day == saves.DaysInGame + 1)
            {
                _tomorrowReward = presenter;
                break;
            }

            if (day == saves.DaysInGame)
                _todayReward = presenter;
            else
                presenter.SetClearAnimation();

            day++;
        }

        if (_todayReward == null)
        {
            _tomorrowReward.SetFocusAnimation();
            return;
        }

        _todayRewardPanel.gameObject.SetActive(true);
        _todayRewardPanel.Render(_todayReward.Data);

        _todayReward.Data.Action.AddBonus();
    }
}
