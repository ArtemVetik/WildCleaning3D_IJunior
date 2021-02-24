using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class PurchaseButton : MonoBehaviour, IPurchasingComponent
{
    protected PurchaseManager PurchaseManager { get; private set; }

    private Button _purchaseButton;

    private void Awake()
    {
        _purchaseButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
        PurchaseManager.OnPurchaseConsumable += OnPurchaseConsumable;
        PurchaseManager.OnPurchaseNonConsumable += OnPurchaseNonConsumable;
    }

    private void OnDisable()
    {
        _purchaseButton.onClick.RemoveListener(OnPurchaseButtonClicked);
        PurchaseManager.OnPurchaseConsumable -= OnPurchaseConsumable;
        PurchaseManager.OnPurchaseNonConsumable -= OnPurchaseNonConsumable;
    }

    public void Init(PurchaseManager purchaseManager)
    {
        PurchaseManager = purchaseManager;
    }

    protected abstract void OnPurchaseConsumable(PurchaseEventArgs args);
    protected abstract void OnPurchaseNonConsumable(PurchaseEventArgs args);
    protected abstract void OnPurchaseButtonClicked();
}
