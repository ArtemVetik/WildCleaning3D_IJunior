using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicInfoPresenter : MonoBehaviour
{
    [SerializeField] private PlayerCharacteristics _characteristics;
    [SerializeField] private CharacteristicPresenter _speedPresenter;
    [SerializeField] private CharacteristicPresenter _cleanlinessPresenter;

    private void Start()
    {
        var playerData = _characteristics.Characteristic;
        playerData.Load(new JsonSaveLoad());

        _speedPresenter.Render(playerData.Speed, playerData.MaxSpeed, 0);
        _cleanlinessPresenter.Render(playerData.Cleanliness, playerData.MaxCleanliness, 0);
    }
}
