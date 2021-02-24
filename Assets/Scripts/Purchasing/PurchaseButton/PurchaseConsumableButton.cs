using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public abstract class PurchaseConsumableButton : PurchaseButton
{
    [SerializeField] private PurchasingProductDefinition.ConsumableID _productId;

    protected override sealed void OnPurchaseButtonClicked()
    {
        var productId = PurchasingProductDefinition.Consumable.GetByID(_productId);
        PurchaseManager.BuyConsumable(productId);
    }

    protected override sealed void OnPurchaseConsumable(PurchaseEventArgs args)
    {
        var productId = PurchasingProductDefinition.Consumable.GetByID(_productId);
        if (args.purchasedProduct.definition.id != productId)
            return;

        OnPurchasingCompleted(_productId);
    }

    protected abstract void OnPurchasingCompleted(PurchasingProductDefinition.ConsumableID productId);
    protected override sealed void OnPurchaseNonConsumable(PurchaseEventArgs args) { }
}
