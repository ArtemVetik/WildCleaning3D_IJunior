using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosterInventoryPresenter : MonoBehaviour
{
    [SerializeField] private Image _preview;
    [SerializeField] private Text _name;
    [SerializeField] private Text _description;
    [SerializeField] private Text _count;

    public void Render(BoosterData data, int count)
    {
        _preview.sprite = data.Preview;
        _name.text = data.Name;
        _description.text = data.Description;
        _count.text = count.ToString();
    }
}
