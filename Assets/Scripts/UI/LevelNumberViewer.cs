using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class LevelNumberViewer : MonoBehaviour
{
    [SerializeField] private CurrentLevelLoader _levelLoader;

    private TMP_Text _levelNumber;

    private void Awake()
    {
        _levelNumber = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _levelNumber.text = (_levelLoader.LevelIndex + 1).ToString();
    }
}
