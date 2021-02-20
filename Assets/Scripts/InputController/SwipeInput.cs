using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInput : BaseInput
{
    private Vector2 _startPosition;
    private float _minSwipeDistance = 100f;
    private float _maxDeltaTime = .2f;
    private float _startTouchTime;

    private void Update()
    {
        if (Input.touchCount == 0)
            return;

        var firstTouch = Input.GetTouch(0);
        if (firstTouch.fingerId != 0)
            return;

        if (firstTouch.phase == TouchPhase.Began)
        {
            _startPosition = firstTouch.position;
            _startTouchTime = Time.time;
        }
        else if (firstTouch.phase == TouchPhase.Moved)
        {
            float swipeDistance = Vector2.Distance(firstTouch.position, _startPosition);
            float deltaTime = Time.time - _startTouchTime;

            if (swipeDistance >= _minSwipeDistance && deltaTime < _maxDeltaTime)
            {
                var deltaPosition = firstTouch.position - _startPosition;
                if (Mathf.Abs(deltaPosition.x) > Mathf.Abs(deltaPosition.y))
                    deltaPosition.y = 0;
                else
                    deltaPosition.x = 0;

                deltaPosition.Normalize();
                var direction = new Vector2Int((int)deltaPosition.x, (int)deltaPosition.y);
                Move(direction);

                _startTouchTime = float.MinValue;
            }
        }
    }
}
