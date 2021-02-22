using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using System.Linq;
using UnityEngine;

public class LanguageSettingsPanel : MonoBehaviour
{
    private LanguageButton[] _languageButtons;
    private LanguageButton _activeLanguage;

    private void Awake()
    {
        _languageButtons = GetComponentsInChildren<LanguageButton>();
    }

    private void OnEnable()
    {
        foreach (var languageButton in _languageButtons)
            languageButton.ButtonClicked += OnLanguageButtonClicked;
    }

    private void Start()
    {
        var currentLanguage = LocalizationManager.Language;
        _activeLanguage = _languageButtons.First(button => button.Language == currentLanguage);
        _activeLanguage.Select();
    }

    private void OnLanguageButtonClicked(LanguageButton button)
    {
        _activeLanguage.Deselect();

        button.Select();
        _activeLanguage = button;

        LocalizationManager.Language = _activeLanguage.Language;
    }

    private void OnDisable()
    {
        foreach (var languageButton in _languageButtons)
            languageButton.ButtonClicked -= OnLanguageButtonClicked;
    }
}
