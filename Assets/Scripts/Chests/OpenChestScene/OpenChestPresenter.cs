using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenChestPresenter : MonoBehaviour
{
    [SerializeField] private OpenChestSceneLoad _sceneLoader;
    [SerializeField] private ChestDataBase _dataBase;
    [SerializeField] private ResultPresenter _resultPresenter;
    [SerializeField] private Text _name;
    [SerializeField] private Image _preview;
    [SerializeField] private Button _openButton;

    private void OnEnable()
    {
        _openButton.onClick.AddListener(OnOpenButtonClicked);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(OnOpenButtonClicked);
    }

    private void Start()
    {
        Chest chest = _sceneLoader.Chest;
        _name.text = chest.Name;
        _preview.sprite = chest.Preview;
    }

    private void OnOpenButtonClicked()
    {
        ChestInventory inventory = new ChestInventory(_dataBase);
        inventory.Load(new JsonSaveLoad());

        gameObject.SetActive(false);

        var opener = new ChestOpener(_sceneLoader.Chest);
        var randomItem = opener.GetRandomItem();
        randomItem.Action.Use();

        inventory.Remove(_sceneLoader.Chest);
        inventory.Save(new JsonSaveLoad());

        _resultPresenter.Present(randomItem);
    }
}
