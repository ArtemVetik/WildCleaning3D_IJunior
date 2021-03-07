using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusShower : MonoBehaviour
{
    [SerializeField] private DailyBonusTableRender _table;

    private void Start()
    {
        if (DailyBonusService.TryGetBonus())
        {
            _table.gameObject.SetActive(true);
        }
    }
}
