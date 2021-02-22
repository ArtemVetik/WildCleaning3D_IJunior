using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine;

namespace Assets.SimpleLocalization
{
    [Serializable]
    public struct LocalizeImage
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite;

        public LocalizeImage(string name, Sprite sprite = null)
        {
            _name = name;
            _sprite = sprite;
        }

        public string Name => _name;
        public Sprite Sprite => _sprite;
    }

    [RequireComponent(typeof(Image))]
    public class LocalizedImage : MonoBehaviour
    {
        [SerializeField] public List<LocalizeImage> Images = new List<LocalizeImage>();

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            LocalizationManager.LocalizationChanged += LocalizeImage;
        }

        private void OnDisable()
        {
            LocalizationManager.LocalizationChanged -= LocalizeImage;
        }

        private void Start()
        {
            LocalizeImage();
        }

        private void LocalizeImage()
        {
            var language = LocalizationManager.Language;
            _image.sprite = Images.Find(font => font.Name == language).Sprite;
        }
    }
}
