using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _count;

    public void Render(int count)
    {
        _count.text = count.ToString();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
