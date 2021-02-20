using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;

public class CleanerShopMenu : MonoBehaviour
{
    public void GoHome()
    {
        GameScene.Load();
    }
}
