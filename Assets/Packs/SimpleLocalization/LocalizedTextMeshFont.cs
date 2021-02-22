using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;

namespace Assets.SimpleLocalization
{
    [Serializable]
    public struct LocalizeFont
    {
        [SerializeField] private string _name;
        [SerializeField] private TMP_FontAsset _font;

        public LocalizeFont(string name, TMP_FontAsset font = null)
        {
            _name = name;
            _font = font;
        }

        public string Name => _name;
        public TMP_FontAsset Font => _font;
    }

    [RequireComponent(typeof(TMP_Text))]
    public class LocalizedTextMeshFont : MonoBehaviour
    {
        [SerializeField] public List<LocalizeFont> Fonts = new List<LocalizeFont>();

        private TMP_Text _textMesh;

        private void Awake()
        {
            _textMesh = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            LocalizationManager.LocalizationChanged += LocalizeFont;
        }

        private void OnDisable()
        {
            LocalizationManager.LocalizationChanged -= LocalizeFont;
        }

        private void Start()
        {
            LocalizeFont();
        }

        private void LocalizeFont()
        {
            var language = LocalizationManager.Language;
            _textMesh.font = Fonts.Find(font => font.Name == language).Font;
        }
    }
}
