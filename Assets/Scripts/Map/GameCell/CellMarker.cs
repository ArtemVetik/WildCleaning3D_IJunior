using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CellMarker : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _dirtySprite;

    private Coroutine _fillCoroutine;

    public event UnityAction Marked;
    public event UnityAction Unmarked;

    private void Start()
    {
        _dirtySprite.color = Color.white;
    }

    public void Mark()
    {
        if (_fillCoroutine != null)
            StopCoroutine(_fillCoroutine);

        _fillCoroutine = StartCoroutine(FillFloor(new Color(0,0,0,0)));
        Marked?.Invoke();
    }

    public void Unmark()
    {
        if (_fillCoroutine != null)
            StopCoroutine(_fillCoroutine);

        _fillCoroutine = StartCoroutine(FillFloor(Color.white));
        Unmarked?.Invoke();
    }

    private IEnumerator FillFloor(Color targetColor)
    {
        while (_dirtySprite.color != targetColor)
        {
            _dirtySprite.color = Color.Lerp(_dirtySprite.color, targetColor, 2f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
