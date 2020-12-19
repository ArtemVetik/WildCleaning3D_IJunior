using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using CustomRedactor;

[Serializable]
public struct CellData
{
    [SerializeField] private LevelObject _levelObject;
    [SerializeField] private ObjectParameters _parameters;

    public LevelObject LevelObject => _levelObject;
    public ObjectParameters Parameters => _parameters;

    public CellData(LevelObject levelObject, ObjectParameters parameters)
    {
        _levelObject = levelObject;

        if (parameters && parameters.CanApply(levelObject))
            _parameters = parameters;
        else
            _parameters = null;
    }
}

[Serializable]
public class LevelDictionary : SerializableDictionary<Vector2Int, CellData> { }

[Serializable]
public class LevelData
{
    [SerializeField, HideInInspector] public Vector2Int Size;
    [SerializeField, HideInInspector] public LevelDictionary Map = new LevelDictionary();
    [SerializeField, HideInInspector] public List<Vector2Int> KeyStagesPoint = new List<Vector2Int>() { new Vector2Int(2,1), new Vector2Int(123,-123) };
}