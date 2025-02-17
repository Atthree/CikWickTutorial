using UnityEngine;

[CreateAssetMenu(fileName = "WheatDesignSO", menuName = "ScriptableObject/WheatDesignSO")]
public class WheatDesignSO : ScriptableObject
{
    [SerializeField] private float _increaseDecreaseMultiplier;
    [SerializeField] private float _resetBoostDuration;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _pasiveSprite;
    [SerializeField] private Sprite _activeWheatSprite;
    [SerializeField] private Sprite _pasiveWheatSprite;

    public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;
    public float ResetBoostDuration => _resetBoostDuration;
    public Sprite ActiveSprite => _activeSprite;
    public Sprite PasiveSprite => _pasiveSprite;
    public Sprite ActiveWheatSprite => _activeWheatSprite;
    public Sprite PasiveWheatSprite => _pasiveWheatSprite;
}
