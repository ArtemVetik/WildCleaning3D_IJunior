using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PurchasingProductDefinition
{
    public enum NonConsumableID
    {
        RemoveAds
    }

    public enum ConsumableID
    {
        Gems10, Gems35, Gems100
    }

    public static class NonConsumable
    {
        public static string GetByID(NonConsumableID id) => id.ToString();

        public static string[] GetAll()
        {
            var enumValues = Enum.GetValues(typeof(NonConsumableID)).Cast<NonConsumableID>();
            return enumValues.Select(value => value.ToString()).ToArray();
        }
    }

    public static class Consumable
    {
        public static string GetByID(ConsumableID id) => id.ToString();

        public static string[] GetAll()
        {
            var enumValues = Enum.GetValues(typeof(ConsumableID)).Cast<ConsumableID>();
            return enumValues.Select(value => value.ToString()).ToArray();
        }
    }
}
