using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Canvas))]
public class GameCanvas : MonoBehaviour
{
    private Canvas _canvas;

    public event UnityAction Enabled;
    public event UnityAction Disabled;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void Show()
    {
        _canvas.enabled = true;
        Enabled?.Invoke();
    }

    public void Hide()
    {
        _canvas.enabled = false;
        Disabled?.Invoke();
    }
}
