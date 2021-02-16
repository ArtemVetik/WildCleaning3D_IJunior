using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _focus;

    private TabButton[] _tabs;
    private Coroutine _focusMoving;
    private TabButton _currentTab;

    private void Awake()
    {
        _tabs = GetComponentsInChildren<TabButton>();
    }

    private void OnEnable()
    {
        foreach (var tab in _tabs)
            tab.ButtonClicked += OnTabClicked;
    }

    private void Start()
    {
        OnTabClicked(_tabs[1]);
    }

    private void OnTabClicked(TabButton tab)
    {
        if (_currentTab != null)
            _currentTab.SetInactive();

        _currentTab = tab;
        _currentTab.SetActive();
        _focus.SetParent(tab.transform);

        if (_focusMoving != null)
            StopCoroutine(_focusMoving);

        _focusMoving = StartCoroutine(FocusMoving());
    }

    private IEnumerator FocusMoving()
    {
        while (_focus.anchoredPosition.x != 0)
        {
            float focusX = Mathf.Lerp(_focus.anchoredPosition.x, 0, 10f * Time.deltaTime);
            _focus.anchoredPosition = new Vector2(focusX, -50);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnDisable()
    {
        foreach (var tab in _tabs)
            tab.ButtonClicked -= OnTabClicked;
    }
}
