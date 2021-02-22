using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using TMPro;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocalizedTextMeshFont))]
public class LocalizedTextMeshEditor : Editor
{
    private LocalizedTextMeshFont _textMeshFont;
    private Dictionary<string, Dictionary<string, string>>.KeyCollection _languages;

    private void OnEnable()
    {
        _textMeshFont = target as LocalizedTextMeshFont;

        LocalizationManager.Read();
        _languages = LocalizationManager.Dictionary.Keys;

        if (_languages.Count != _textMeshFont.Fonts.Count)
        {
            var newFontArray = new List<LocalizeFont>();
            int i = 0;
            foreach (var language in _languages)
            {
                TMP_FontAsset font = null;
                if (_textMeshFont.Fonts.Count > i)
                    font = _textMeshFont.Fonts[i].Font;

                newFontArray.Add(new LocalizeFont(language, font));
                i++;
            }

            _textMeshFont.Fonts = newFontArray;
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var fontsArray = serializedObject.FindProperty(nameof(_textMeshFont.Fonts));

        for (int i = 0; i < fontsArray.arraySize; i++)
        {
            var arrayItem = fontsArray.GetArrayElementAtIndex(i);
            var font = arrayItem.FindPropertyRelative("_font");
            EditorGUILayout.PropertyField(font, new GUIContent(_textMeshFont.Fonts[i].Name));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
