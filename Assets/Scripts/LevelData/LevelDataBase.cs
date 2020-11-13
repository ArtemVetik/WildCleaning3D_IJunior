using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Date Base", menuName = "LevelDateBase", order = 51)]
public class LevelDataBase : ScriptableObject
{
    [SerializeField, HideInInspector] public List<LevelData> _levels = new List<LevelData>();

    public LevelData this[int index] => _levels[index];
    public bool EmptyOrNull => _levels == null || _levels.Count == 0;
    public int Count => _levels.Count;

    public void Add()
    {
        _levels.Add(new LevelData());
    }

    public void Remove(LevelData levelData)
    {
        _levels.Remove(levelData);
    }

    public int IndexOf(LevelData levelData)
    {
        return _levels.IndexOf(levelData);
    }

    public void Clear()
    {
        _levels.Clear();
    }
}
