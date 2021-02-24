using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseGemsButton : PurchaseConsumableButton
{
    protected override void OnPurchasingCompleted(PurchasingProductDefinition.ConsumableID productId)
    {
        int gems = GetValueByProduct(productId);

        DiamondBalance balance = new DiamondBalance();
        balance.Load(new JsonSaveLoad());
        balance.AddDiamond(gems);
        balance.Save(new JsonSaveLoad());
    }

    private int GetValueByProduct(PurchasingProductDefinition.ConsumableID productId)
    {
        if (productId == PurchasingProductDefinition.ConsumableID.Gems10)
            return 10;
        if (productId == PurchasingProductDefinition.ConsumableID.Gems35)
            return 35;
        if (productId == PurchasingProductDefinition.ConsumableID.Gems100)
            return 100;

        return 0;
    }
}
