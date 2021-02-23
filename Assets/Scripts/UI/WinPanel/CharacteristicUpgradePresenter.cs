using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicUpgradePresenter : MonoBehaviour
{
    [SerializeField] private CleanersDataBase _dataBase;
    [SerializeField] private PlayerInitializer _playerInitialized;
    [SerializeField] private UpgradePlayer _upgrader;
    [Header("Presenters")]
    [SerializeField] private Image _cleanerPreview;
    [SerializeField] private CharacteristicPresenter _speedPresenter;
    [SerializeField] private CharacteristicPresenter _cleanlinessPresenter;

    private void OnEnable()
    {
        _upgrader.PlayerUpgraded += OnPlayerUpgraded;
    }

    private void OnPlayerUpgraded(IPlayerData oldData, IPlayerData newData)
    {
        var player = _playerInitialized.InstPlayer;
        var playerData = _dataBase.Data.First((data) => data.Prefab.Cleaner.DefaultCharacteristics.ID == player.DefaultCharacteristics.ID);

        _cleanerPreview.sprite = playerData.Preview;

        _speedPresenter.Render(newData.Speed, newData.MaxSpeed, newData.Speed - oldData.Speed);
        _cleanlinessPresenter.Render(newData.Cleanliness, newData.MaxCleanliness, newData.Cleanliness - oldData.Cleanliness);
    }

    private void OnDisable()
    {
        _upgrader.PlayerUpgraded -= OnPlayerUpgraded;
    }
}
