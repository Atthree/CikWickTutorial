using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [Header("Refenrences")]
    [SerializeField] private RectTransform _timerRotatableTransform;
    [SerializeField] private TMP_Text _timerText;

    [Header("Settings")]

    [SerializeField] private float _rotationDuration;
    [SerializeField] private Ease _rotationEase;

    private float _elapsTİme = 0f;

    private void Start()
    {
        PlayRotationAnimation();
        StarTimer();
    }

    private void PlayRotationAnimation()
    {
        _timerRotatableTransform.DORotate(new Vector3(0f, 0f, -360f), _rotationDuration, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(_rotationEase);
    }

    private void StarTimer(){
        _elapsTİme = 0f;
        InvokeRepeating(nameof(UpdateTimerUI),0f,1f);
    }

    private void UpdateTimerUI()
    {
        _elapsTİme += 1f;
        int minutes = Mathf.FloorToInt(_elapsTİme / 60f);
        int seconds = Mathf.FloorToInt(_elapsTİme % 60f);

        _timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    } 
}
