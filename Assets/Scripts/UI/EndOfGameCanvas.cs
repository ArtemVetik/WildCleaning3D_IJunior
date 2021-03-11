using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class EndOfGameCanvas : MonoBehaviour
{
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private LoosePanel _loosePanel;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void ShowWin()
    {
        _canvas.enabled = true;
        _loosePanel.Hide();
        _winPanel.Show();
    }

    public void ShowLoose()
    {
        _canvas.enabled = true;
        _winPanel.Hide();
        _loosePanel.Show();
    }

    public void Hide()
    {
        _canvas.enabled = false;
    }
}
