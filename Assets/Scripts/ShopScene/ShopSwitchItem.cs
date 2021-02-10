using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum ShopType
{
    CleanerShop, BoosterShop, ChestShop
}

[Serializable]
public class ShopSwitchItem
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private ShopViewer _viewer;
    [SerializeField] private ShopType _shopType;

    public ShopType ShopType => _shopType;

    public void Activate()
    {
        _canvas.gameObject.SetActive(true);
        _viewer.Activate();
    }

    public void Deactivate()
    {
        _canvas.gameObject.SetActive(false);
        _viewer.Deactivate();
    }
}
