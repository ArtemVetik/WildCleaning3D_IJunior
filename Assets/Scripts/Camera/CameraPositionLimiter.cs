using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionLimiter : MonoBehaviour
{
    [SerializeField] private GameObject _roomHolder;

    public float XMax { get; private set; }
    public float ZMin { get; private set; }

    private void Start()
    {
        var room = _roomHolder.GetComponentInChildren<Room>();
        var bounds = room.Bounds;

        XMax = bounds.max.x + 4;
        ZMin = bounds.min.z - 7;
    }
}
