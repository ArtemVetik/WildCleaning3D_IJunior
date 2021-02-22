using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using UnityEngine;

public class LanguageSaves : MonoBehaviour
{
    private const string SaveKey = "GameLanguage";

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey(SaveKey) == false)
            PlayerPrefs.SetString(SaveKey, "English");

        var language = PlayerPrefs.GetString(SaveKey);
        LocalizationManager.Language = language;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetString(SaveKey, LocalizationManager.Language);
    }
}
