using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPresenter : MonoBehaviour
{
    [SerializeField] private Text _rewardedText;

    public void Present(ChestItem item)
    {
        gameObject.SetActive(true);

        _rewardedText.text = item.Action.RewardedInfo;
    }
}
