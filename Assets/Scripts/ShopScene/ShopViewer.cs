using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopViewer : MonoBehaviour
{
    [SerializeField] private CameraTargetFolowing _cameraFolowing;

    public void Activate()
    {
        _cameraFolowing.enabled = true;
    }

    public void Deactivate()
    {
        _cameraFolowing.enabled = false;
    }

    public void SetCameraTarget(Transform target)
    {
        _cameraFolowing.SetTarget(target);
    }
}
