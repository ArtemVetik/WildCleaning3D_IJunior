using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    private Chest _chest;

    public ChestOpener(Chest chest)
    {
        _chest = chest;
    }

    public ChestItem GetRandomItem()
    {
        float randomValue = Random.Range(0f, 1f);

        ChestItem randomItem = new ChestItem();
        float probability = 0f;

        foreach (var item in _chest.Items)
        {
            randomItem = item;
            probability += item.Probability;

            if (probability >= randomValue)
                return item;
        }

        return randomItem;
    }
}
