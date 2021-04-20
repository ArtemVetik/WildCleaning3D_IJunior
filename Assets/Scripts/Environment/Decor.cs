using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decor : MonoBehaviour
{
    [SerializeField] private int _targetLevel;

    public int TargetLevel => _targetLevel;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
