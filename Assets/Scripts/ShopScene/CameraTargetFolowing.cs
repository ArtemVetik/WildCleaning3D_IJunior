using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetFolowing : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _yAngle;
    [SerializeField] private float _topDownAngle;
    [SerializeField] private float _distanceToTarget;
    [Header("Shifts")]
    [SerializeField] private float yShift;

    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null)
            return;

        var _cameraShift = CalculateCameraShift();
        _cameraShift.y += yShift;

        Vector3 targetPosition = _target.transform.position + _cameraShift;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, _speed * Time.deltaTime);

        Vector3 targetRotation = CalculateCameraEulerAngles();
        Camera.main.transform.eulerAngles = Vector3.Lerp(Camera.main.transform.eulerAngles, targetRotation, _speed * Time.deltaTime);
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
        return new Vector3(0.7f * _topDownAngle, _yAngle, Camera.main.transform.eulerAngles.z);
    }
}
