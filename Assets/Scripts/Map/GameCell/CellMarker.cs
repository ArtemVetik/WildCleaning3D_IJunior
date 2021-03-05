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
    public event UnityAction Unmarked;

    private void Start()
    {
        _dirtySprite.color = Color.white;
    }

    public void Mark(Type type = Type.Normal)
    {
        if (_fillCoroutine != null)
            StopCoroutine(_fillCoroutine);

        _fillCoroutine = StartCoroutine(FillFloor(new Color(0,0,0,0)));
        Marked?.Invoke(type);
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
