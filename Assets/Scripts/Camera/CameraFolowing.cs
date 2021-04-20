using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolowing : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _yAngle;
    [SerializeField] private float _topDownAngle;
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private Vector3 _shift;
    [SerializeField] private PlayerInitializer _playerInitializer;
    [SerializeField] private CameraPositionLimiter _limiter;
    [SerializeField] private BaseInput _keyboardInput;
    [SerializeField] private BaseInput _swipeInput;

    private Player _player;
    private Vector3 _cameraShift;
    private Vector3 _playerDirection;

    private void OnEnable()
    {
        _playerInitializer.PlayerInitialized += OnPlayerInitialize;
#if UNITY_EDITOR
        _keyboardInput.ScalingChanged += OnScalingChanged;
#else
        _swipeInput.ScalingChanged += OnScalingChanged;
#endif
    }

    private void OnDisable()
    {
        _playerInitializer.PlayerInitialized -= OnPlayerInitialize;
#if UNITY_EDITOR
        _keyboardInput.ScalingChanged -= OnScalingChanged;
#else
        _swipeInput.ScalingChanged -= OnScalingChanged;
#endif
    }

    private void LateUpdate()
    {
        if (_player == null)
            return;

        _cameraShift = CalculateCameraShift();
        transform.eulerAngles = CalculateCameraEulerAngles();

        Vector3 nextPlayerDirection = new Vector3(_player.Direction.x, 0, _player.Direction.y) * _distanceToPlayer / 20;
        _playerDirection = Vector3.Lerp(_playerDirection, nextPlayerDirection, _speed * Time.deltaTime);

        Vector3 targetPosition = _player.transform.position + _cameraShift;// + _playerDirection * 2f;
        float xClamp = Mathf.Clamp(targetPosition.x, float.MinValue, _limiter.XMax);
        float zClamp = Mathf.Clamp(targetPosition.z, _limiter.ZMin, float.MaxValue);
        targetPosition = new Vector3(xClamp, targetPosition.y, zClamp);

        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
    }

    private void OnPlayerInitialize(Player player)
    {
        _player = player;
        _cameraShift = CalculateCameraShift();

        transform.eulerAngles = CalculateCameraEulerAngles();
    }

    private void OnScalingChanged(float delta)
    {
        _distanceToPlayer -= delta;
        _distanceToPlayer = Mathf.Clamp(_distanceToPlayer, 2f, 30f);
    }

    private Vector3 CalculateCameraShift()
    {
        float height = _distanceToPlayer * Mathf.Sin(_topDownAngle * Mathf.Deg2Rad);
        float linearDistance = _distanceToPlayer * Mathf.Cos(_topDownAngle * Mathf.Deg2Rad);

        float z = Mathf.Cos(_yAngle * Mathf.Deg2Rad);
        float x = Mathf.Sin(_yAngle * Mathf.Deg2Rad);

        Vector3 cameraShift = new Vector3(x, 0, z) * linearDistance * -1;
        cameraShift += Vector3.up * height + _shift;

        return cameraShift;
    }

    private Vector3 CalculateCameraEulerAngles()
    {
        return new Vector3(_topDownAngle, _yAngle, transform.eulerAngles.z);
    }
}
