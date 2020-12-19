using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLevelLoader : MonoBehaviour
{
    [SerializeField] private LevelDataBase _levelDataBase;

    public int CurrentLevelIndex = 0;

    public LevelData CurrentLevel => _levelDataBase[CurrentLevelIndex];
}
