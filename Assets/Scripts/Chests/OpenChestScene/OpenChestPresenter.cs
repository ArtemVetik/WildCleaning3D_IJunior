using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenChestPresenter : MonoBehaviour
{
    [SerializeField] private OpenChestSceneStartPoint _sceneLoader;
    [SerializeField] private ChestDataBase _dataBase;
    [SerializeField] private Animator _chestAnimator;
    [SerializeField] private FromToTransformLerp _camera;
    [SerializeField] private GameObject _fireworks;
    [SerializeField] private Button _openButton;

    private ChestItem _item;

    private void OnEnable()
    {
        _openButton.onClick.AddListener(OnOpenButtonClicked);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(OnOpenButtonClicked);
    }

    private void OnOpenButtonClicked()
    {
        ChestInventory inventory = new ChestInventory(_dataBase);
        inventory.Load(new JsonSaveLoad());

        var opener = new ChestOpener(_sceneLoader.Chest);
        var randomItem = opener.GetRandomItem();
        _item = randomItem;
        randomItem.Action.Use();
        _openButton.gameObject.SetActive(false);

        inventory.Remove(_sceneLoader.Chest);
        inventory.Save(new JsonSaveLoad());

        _chestAnimator.SetTrigger("Open");
        _camera.StartLerp();
    }

    private void OnChestOpened()
    {
        var effect = Instantiate(_item.Action.ShowEffect, transform.position, _item.Action.ShowEffect.transform.rotation);

        effect.RewardShown += OnRewardedShown;
    }

    private void OnRewardedShown(ShowRewardAnimation effect)
    {
        _fireworks.SetActive(true);
        effect.RewardShown -= OnRewardedShown;
    }
}
