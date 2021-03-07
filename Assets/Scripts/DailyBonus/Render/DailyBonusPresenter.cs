using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _day;
    [SerializeField] private Image _preview;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private DailyBonusPresenterAnimation _animation;

    private Coroutine _clearCoroutine;
    private Coroutine _focusCoroutine;

    public DailyBonusData Data { get; private set; }

    public void Render(DailyBonusData data)
    {
        Data = data;

        _day.text = $"Day {data.Day}";
        _preview.sprite = data.Preview;
        _value.text = data.BonusValue.ToString();
    }

    public void SetClearAnimation()
    {
        if (_clearCoroutine != null)
            StopCoroutine(_clearCoroutine);

        _clearCoroutine = StartCoroutine(ClearAnimation(.5f));
    }

    public void SetFocusAnimation()
    {
        if (_focusCoroutine != null)
            StopCoroutine(_focusCoroutine);

        _focusCoroutine = StartCoroutine(FocusAnimation(1f));
    }

    private IEnumerator ClearAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        _animation.SetTrigger(DailyBonusPresenterAnimation.Parameters.ShowClear);
    }

    private IEnumerator FocusAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        _animation.SetTrigger(DailyBonusPresenterAnimation.Parameters.ShowFocus);
    }
}