using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromToTransformLerp : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _to;

    private bool _canMove = false;

    public void StartLerp()
    {
        _canMove = true;
    }

    private void Update()
    {
        if (_canMove == false)
            return;

        transform.position = Vector3.Lerp(transform.position, _to.position, _speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _to.rotation, _speed * Time.deltaTime);
    }
}
