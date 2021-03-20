using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInput : BaseInput
{
    private readonly float ScreenDiagonal = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height);
    private float _minSwipeDistance;
    //private float _maxDeltaTime = .2f;
    //private float _startTouchTime;
    private Vector2 _startPosition;
    private float _lastScaleValue;
    private Vector2[] _startScalingPositions = new Vector2[2];

    private void Start()
    {
        _minSwipeDistance = 0.05f * Screen.height;
    }

    private void Update()
    {
        if (Input.touchCount == 1)
            Swipe();

        if (Input.touchCount == 2)
            Scaling();
    }

    private void Scaling()
    {
        var firstFinger = Input.GetTouch(0);
        var secondFinger = Input.GetTouch(1);

        if (firstFinger.fingerId != 0 || secondFinger.fingerId != 1)
            return;

        if (secondFinger.phase == TouchPhase.Began)
        {
            _startScalingPositions[0] = firstFinger.position;
            _startScalingPositions[1] = secondFinger.position;
        }

        if (_startScalingPositions[0] != Vector2.zero && _startScalingPositions[1] != Vector2.zero)
        {
            var oldDistance = Vector2.Distance(_startScalingPositions[0], _startScalingPositions[1]) / ScreenDiagonal * 20;
            var newDistance = Vector2.Distance(firstFinger.position, secondFinger.position) / ScreenDiagonal * 20;

            var scaling = newDistance - oldDistance;
            Scaling(scaling - _lastScaleValue);

            _lastScaleValue = scaling;
        }

        if (firstFinger.phase == TouchPhase.Ended || secondFinger.phase == TouchPhase.Ended)
        {
            _lastScaleValue = 0;
            _startScalingPositions = new Vector2[2] { Vector2.zero, Vector2.zero};
        }
    }

    private void Swipe()
    {
        var firstTouch = Input.GetTouch(0);
        if (firstTouch.fingerId != 0)
            return;

        if (firstTouch.phase == TouchPhase.Began)
        {
            _startPosition = firstTouch.position;
            //_startTouchTime = Time.time;
        }
        else if (firstTouch.phase == TouchPhase.Moved)
        {
            float swipeDistance = Vector2.Distance(firstTouch.position, _startPosition);
            //float deltaTime = Time.time - _startTouchTime;

            if (swipeDistance >= _minSwipeDistance /* && deltaTime < _maxDeltaTime */)
            {
                var deltaPosition = firstTouch.position - _startPosition;
                if (Mathf.Abs(deltaPosition.x) > Mathf.Abs(deltaPosition.y))
                    deltaPosition.y = 0;
                else
                    deltaPosition.x = 0;

                deltaPosition.Normalize();
                var direction = new Vector2Int((int)deltaPosition.x, (int)deltaPosition.y);
                Move(direction);

                _startPosition = firstTouch.position;
                //_startTouchTime = float.MinValue;
            }
        }
    }
}
