using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UICanvas : MonoBehaviour
{
    [SerializeField] private BaseInput[] _inputs;

    private PopupPanel[] _childPanels;
    private List<PopupPanel> _panelsBuffer;
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _childPanels = GetComponentsInChildren<PopupPanel>(true);
        _panelsBuffer = new List<PopupPanel>();
    }

    private void OnEnable()
    {
        foreach (var panel in _childPanels)
        {
            panel.Opened += OnPanelOpened;
            panel.Closed += OnPanelClosed;
        }
    }

    private void OnPanelOpened(PopupPanel panel)
    {
        _panelsBuffer.Add(panel);
        DisableInputs();
    }

    private void OnPanelClosed(PopupPanel panel)
    {
        _panelsBuffer.Remove(panel);

        if (_panelsBuffer.Count == 0)
            EnableInputs();
    }

    private void EnableInputs()
    {
        foreach (var input in _inputs)
            if (input)
                input.enabled = true;
    }

    private void DisableInputs()
    {
        foreach (var input in _inputs)
            if (input)
                input.enabled = false;
    }

    public void Hide()
    {
        _canvas.enabled = false;
    }

    private void OnDisable()
    {
        foreach (var panel in _childPanels)
        {
            panel.Opened -= OnPanelOpened;
            panel.Closed -= OnPanelClosed;
        }
    }
}
