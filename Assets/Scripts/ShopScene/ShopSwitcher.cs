using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class ShopSwitcher : MonoBehaviour, ISceneLoadHandler<ShopType>
{
    [SerializeField] private List<ShopSwitchItem> _shopItems;

    private ShopSwitchItem _currentItem;

    public void Activate(ShopType type)
    {
        if (_currentItem != null)
            _currentItem.Deactivate();

        _currentItem = _shopItems.Find(item => item.ShopType == type);
        _currentItem.Activate();
    }

    public void OnSceneLoaded(ShopType type)
    {
        Activate(type);
    }
}
