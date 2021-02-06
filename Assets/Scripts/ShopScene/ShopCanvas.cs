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

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(OnHomeButtonClicked);
        _cleanerShop.onClick.AddListener(OnCleanerButtonClicked);
        _boosterShop.onClick.AddListener(OnBoosterButtonClicked);
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveListener(OnHomeButtonClicked);
        _cleanerShop.onClick.RemoveListener(OnCleanerButtonClicked);
        _boosterShop.onClick.RemoveListener(OnBoosterButtonClicked);
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
}
