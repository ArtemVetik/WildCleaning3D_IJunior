using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CellMarker : MonoBehaviour
{
    public enum Type
    {
        Normal, Combo,
    }

    [SerializeField] private SpriteRenderer _dirtySprite;

    private Coroutine _fillCoroutine;

    public event UnityAction<Type> Marked;
    public event UnityAction DoubleMarked;
    public event UnityAction Unmarked;

    private void Start()
    {
        _dirtySprite.color = Color.white;
    }

    public void PartiallyMark()
    {
        if (_fillCoroutine != null)
            StopCoroutine(_fillCoroutine);

        _fillCoroutine = StartCoroutine(FillFloor(new Color(0f, 0f, 1f, 1f)));
        Marked?.Invoke(Type.Normal);
    }

    public void SetColor(Color color)
    {
        if (_fillCoroutine != null)
            StopCoroutine(_fillCoroutine);

        _fillCoroutine = StartCoroutine(FillFloor(color));
    }

    public void Mark(Type type = Type.Normal)
    {
        if (_fillCoroutine != null)
            StopCoroutine(_fillCoroutine);

        _fillCoroutine = StartCoroutine(FillFloor(Color.clear));
        Marked?.Invoke(type);
    }

    public void DoubleMark()
    {
        DoubleMarked?.Invoke();
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
            _dirtySprite.color = Color.Lerp(_dirtySprite.color, targetColor, 3f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
