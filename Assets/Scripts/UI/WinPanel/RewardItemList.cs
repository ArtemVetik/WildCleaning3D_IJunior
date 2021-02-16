using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RewardItemList : MonoBehaviour
{
    [SerializeField] private LevelChestSetter _levelChest;
    [SerializeField] private PlayerScore _score;
    [SerializeField] private BoosterContainer _boosterContainer;
    [Header("Presenters")]
    [SerializeField] private RewardPresenter _scorePresenter;
    [SerializeField] private RewardPresenter _diamondPresenter;
    [SerializeField] private RewardPresenter _chestPresenter;
    [SerializeField] private RewardPresenter _antisepticPresenter;
    [SerializeField] private RewardPresenter _speedboostPresenter;
    [SerializeField] private RewardPresenter _waterballPresenter;

    private void OnEnable()
    {
        if (_levelChest.CanAddChest)
        {
            _chestPresenter.Enable();
            _chestPresenter.Render(1);
        }

        _scorePresenter.Render(_score.Score);
        _diamondPresenter.Render(1);

        var boosters = _boosterContainer.CollectedBoosters;

        var antiseptics = boosters.Count(booster => booster is AntisepticObject);
        var speedboost = boosters.Count(booster => booster is SpeedboostObject);
        var waterball = boosters.Count(booster => booster is WatersBallObject);

        if (antiseptics != 0)
        {
            _antisepticPresenter.Enable();
            _antisepticPresenter.Render(antiseptics);
        }
        if (speedboost != 0)
        {
            _speedboostPresenter.Enable();
            _speedboostPresenter.Render(speedboost);
        }
        if (waterball != 0)
        {
            _waterballPresenter.Enable();
            _waterballPresenter.Render(waterball);
        }
    }
}
