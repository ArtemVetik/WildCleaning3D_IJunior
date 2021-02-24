using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseInitializer : MonoBehaviour
{
    private void Awake()
    {
        var purchasingComponents = GetComponentsInChildren<IPurchasingComponent>();
        var instance = Singleton<PurchaseManager>.Instance;

        foreach (var component in purchasingComponents)
            component.Init(instance);
    }
}
