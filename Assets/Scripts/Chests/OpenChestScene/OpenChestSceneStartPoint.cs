using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using IJunior.TypedScenes;

public class OpenChestSceneStartPoint : MonoBehaviour, ISceneLoadHandler<Chest>
{
    public Chest Chest { get; private set; }

    public event UnityAction Loaded;

    public void OnSceneLoaded(Chest chest)
    {
        Chest = chest;
        Loaded?.Invoke();
    }
}
