using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetFolowing : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _yAngle;
    [SerializeField] private float _topDownAngle;
    [SerializeField] private float _distanceToTarget;

    private Transform _target;

    private void Start()
    {
        transform.eulerAngles = CalculateCameraEulerAngles();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null)
            return;

        var _cameraShift = CalculateCameraShift();

        Vector3 targetPosition = _target.transform.position + _cameraShift;
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
    }

    private Vector3 CalculateCameraShift()
    {
        float height = _distanceToTarget * Mathf.Sin(_topDownAngle * Mathf.Deg2Rad);
        float linearDistance = _distanceToTarget * Mathf.Cos(_topDownAngle * Mathf.Deg2Rad);

        float z = Mathf.Cos(_yAngle * Mathf.Deg2Rad);
        float x = Mathf.Sin(_yAngle * Mathf.Deg2Rad);

        Vector3 cameraShift = new Vector3(x, 0, z) * linearDistance * -1;
        cameraShift += Vector3.up * height;

        return cameraShift;
    }

    private Vector3 CalculateCameraEulerAngles()
    {
        return new Vector3(0.7f * _topDownAngle, _yAngle, transform.eulerAngles.z);
    }
}
