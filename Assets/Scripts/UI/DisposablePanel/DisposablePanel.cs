using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposablePanel : MonoBehaviour
{
    [SerializeField] private string _panelID;
    [SerializeField] private List<int> _levels;

    public virtual bool TryOpen(int currentLevel)
    {
        Debug.Log(PlayerPrefs.HasKey(_panelID));
        if (PlayerPrefs.HasKey(_panelID))
            return false;

        if (_levels.Contains(currentLevel))
        {
            gameObject.SetActive(true);
            PlayerPrefs.SetInt(_panelID, 1);
            return true;
        }

        return false;
    }
}
