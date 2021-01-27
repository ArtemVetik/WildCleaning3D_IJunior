using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolowing : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _yAngle;
    [SerializeField] private float _topDownAngle;
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private PlayerInitializer _playerInitializer;


    private Transform _player;
    private Vector3 _cameraShift;

    private void OnEnable()
    {
        _playerInitializer.PlayerInitialized += OnPlayerInitialize;
    }

    private void OnDisable()
    {
        _playerInitializer.PlayerInitialized -= OnPlayerInitialize;
    }

    private void OnPlayerInitialize(Player player)
    {
        _player = player.transform;
        _cameraShift = CalculateCameraShift();

        transform.position = _player.position + _cameraShift;
        transform.eulerAngles = CalculateCameraEulerAngles();
    }

    private void Update()
    {
        if (_player == null)
            return;

        transform.position = Vector3.Lerp(transform.position, _player.position + _cameraShift, _speed * Time.deltaTime);
    }

    private Vector3 CalculateCameraShift()
    {
        float height = _distanceToPlayer * Mathf.Sin(_topDownAngle * Mathf.Deg2Rad);
        float linearDistance = _distanceToPlayer * Mathf.Cos(_topDownAngle * Mathf.Deg2Rad);

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
