using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DailyBonusService
{
    private static readonly string ShowPanelKey = "ShowDailyPanel";
    private static DailyBonusSaves _saves;

    static DailyBonusService()
    {
        _saves = new DailyBonusSaves();
        _saves.Load(new JsonSaveLoad());
    }

    public static bool TryGetBonus()
    {
        var savedDate = DateTime.FromBinary(_saves.BinaryTime);

        var lastDay = new DateTime(savedDate.Year, savedDate.Month, savedDate.Day);
        var currentDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        var subtract = currentDay.Subtract(lastDay);

        if (subtract.Days < 1)
        {
            if (PlayerPrefs.HasKey(ShowPanelKey))
                return false;

            PlayerPrefs.SetInt(ShowPanelKey, 1);
            _saves.Save(new JsonSaveLoad());
            return true;
        }

        PlayerPrefs.DeleteKey(ShowPanelKey);

        if (subtract.Days == 1)
        {
            _saves.AddDayInGame(DateTime.Now);
            _saves.Save(new JsonSaveLoad());
            return true;
        }

        _saves.ResetDaysInGame();
        _saves.Save(new JsonSaveLoad());
        PlayerPrefs.SetInt(ShowPanelKey, 1);
        return true;
    }
}
