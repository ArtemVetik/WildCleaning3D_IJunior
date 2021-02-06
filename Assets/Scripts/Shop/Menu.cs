using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class Menu : MonoBehaviour
{
    public void LoadCleanerShop()
    {
        ShopScene.Load(ShopType.CleanerShop);
    }

    public void LoadBoosterShop()
    {
        ShopScene.Load(ShopType.BoosterShop);
    }
}
