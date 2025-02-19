using DG.Tweening;
using TMPro;
using UnityEngine;

public class EggCounterUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _eggCounterText;

    [Header("Settings")]
    [SerializeField] private Color _eggCounterColor;
    [SerializeField] private float _colorDuration;
    [SerializeField] private float _scaleDuration;
    private RectTransform _eggCounterReactTransform;

    private void Awake()
    {
        _eggCounterReactTransform = _eggCounterText.gameObject.GetComponent<RectTransform>();
    }

    public void SetEggCounter(int counter, int max)
    {
        _eggCounterText.text = counter.ToString() + "/" + max.ToString();
    }

    public void SetEggCompleted()
    {
        _eggCounterText.DOColor(_eggCounterColor, _colorDuration);
        _eggCounterReactTransform.DOScale(1.2f, _scaleDuration).SetEase(Ease.OutBack);
    }

}
