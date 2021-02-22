using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;

public class Menu : MonoBehaviour
{
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadCleanerShop()
    {
        ShopScene.Load(ShopType.CleanerShop);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Assets.SimpleLocalization.LocalizationManager.Language = "Russian";
        if (Input.GetKeyDown(KeyCode.E))
            Assets.SimpleLocalization.LocalizationManager.Language = "English";
    }

    public void LoadBoosterShop()
    {
        ShopScene.Load(ShopType.BoosterShop);
    }
}
