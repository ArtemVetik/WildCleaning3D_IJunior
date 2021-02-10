using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshHeight : MonoBehaviour
{
    public float MaxMeshHeight { get; private set; }

    private void Awake()
    {
        var meshes = GetComponentsInChildren<MeshRenderer>();
        MaxMeshHeight = meshes.Max(mesh => mesh.bounds.size.y);
    }
}
