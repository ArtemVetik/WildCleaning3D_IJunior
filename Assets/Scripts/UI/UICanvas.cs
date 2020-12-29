using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] private BoosterSlotsList _boosterSlots;

    public void Hide()
    {
        _boosterSlots.Apply();
        gameObject.SetActive(false);
    }
}
