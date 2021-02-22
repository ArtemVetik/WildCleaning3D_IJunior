using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocalizedImage))]
public class LocalizedImageEditor : Editor
{
    private LocalizedImage _imageLocalizer;
    private Dictionary<string, Dictionary<string, string>>.KeyCollection _languages;

    private void OnEnable()
    {
        _imageLocalizer = target as LocalizedImage;

        LocalizationManager.Read();
        _languages = LocalizationManager.Dictionary.Keys;

        if (_languages.Count != _imageLocalizer.Images.Count)
        {
            var newFontArray = new List<LocalizeImage>();
            int i = 0;
            foreach (var language in _languages)
            {
                Sprite sprite = null;
                if (_imageLocalizer.Images.Count > i)
                    sprite = _imageLocalizer.Images[i].Sprite;

                newFontArray.Add(new LocalizeImage(language, sprite));
                i++;
            }

            _imageLocalizer.Images = newFontArray;
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var fontsArray = serializedObject.FindProperty(nameof(_imageLocalizer.Images));

        for (int i = 0; i < fontsArray.arraySize; i++)
        {
            var arrayItem = fontsArray.GetArrayElementAtIndex(i);
            var font = arrayItem.FindPropertyRelative("_sprite");
            EditorGUILayout.PropertyField(font, new GUIContent(_imageLocalizer.Images[i].Name));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
