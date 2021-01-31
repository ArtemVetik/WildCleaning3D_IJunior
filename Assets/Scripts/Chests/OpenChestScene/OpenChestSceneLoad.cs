﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class OpenChestSceneLoad : MonoBehaviour, ISceneLoadHandler<Chest>
{
    public Chest Chest { get; private set; }

    public void OnSceneLoaded(Chest chest)
    {
        Chest = chest;
    }
}
