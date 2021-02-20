using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CellSelector : MonoBehaviour
{
    [SerializeField] private Button _cancelButton;

    public event UnityAction<GameCell> Raycasted;
    public event UnityAction Canceled;

    private void OnEnable()
    {
        _cancelButton.onClick.AddListener(OnCancelButtonClick);
    }

    private void OnDisable()
    {
        _cancelButton.onClick.RemoveListener(OnCancelButtonClick);
    }

    private void OnCancelButtonClick()
    {
        Canceled?.Invoke();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryRaycast(Input.mousePosition);
    }

    private void TryRaycast(Vector2 mousePosition)
    {
        var ray = Camera.main.ScreenPointToRay(mousePosition);

        RaycastHit[] hitsInfo = Physics.RaycastAll(ray);
        foreach (var hit in hitsInfo)
        {
            if (hit.collider.TryGetComponent(out GameCell cell))
                Raycasted?.Invoke(cell);
        }
    }
}
