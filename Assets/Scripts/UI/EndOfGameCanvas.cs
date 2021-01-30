using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGameCanvas : MonoBehaviour
{
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private LoosePanel _loosePanel;

    public void ShowWin()
    {
        gameObject.SetActive(true);
        _loosePanel.Hide();
        _winPanel.Show();
    }

    public void ShowLoose()
    {
        gameObject.SetActive(true);
        _winPanel.Hide();
        _loosePanel.Show();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
