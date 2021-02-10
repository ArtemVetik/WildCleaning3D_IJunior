using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestViewer : ShopViewer
{
    [SerializeField] private Transform _presenter;

    private void Start()
    {
        SetCameraTarget(transform);
    }
}
