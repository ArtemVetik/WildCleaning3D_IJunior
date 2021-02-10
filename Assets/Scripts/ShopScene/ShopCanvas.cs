using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvas : MonoBehaviour
{
    [SerializeField] private CleanerShopMenu _menu;
    [SerializeField] private ShopSwitcher _switcher;
    [Header("Buttons")]
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _cleanerShop;
    [SerializeField] private Button _boosterShop;
    [SerializeField] private Button _chestShop;

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(OnHomeButtonClicked);
        _cleanerShop.onClick.AddListener(OnCleanerButtonClicked);
        _boosterShop.onClick.AddListener(OnBoosterButtonClicked);
        _chestShop.onClick.AddListener(OnChestButtonClicked);
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveListener(OnHomeButtonClicked);
        _cleanerShop.onClick.RemoveListener(OnCleanerButtonClicked);
        _boosterShop.onClick.RemoveListener(OnBoosterButtonClicked);
        _chestShop.onClick.RemoveListener(OnChestButtonClicked);
    }

    private void OnHomeButtonClicked()
    {
        _menu.GoHome();
    }

    private void OnCleanerButtonClicked()
    {
        _switcher.Activate(ShopType.CleanerShop);
    }
    private void OnBoosterButtonClicked()
    {
        _switcher.Activate(ShopType.BoosterShop);
    }

    private void OnChestButtonClicked()
    {
        _switcher.Activate(ShopType.ChestShop);
    }
}
