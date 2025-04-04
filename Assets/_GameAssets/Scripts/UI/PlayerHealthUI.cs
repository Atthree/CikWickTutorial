using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image[] _playerHealthImages;
    [Header("Sprites")]
    [SerializeField] private Sprite _playerHealtySprite;
    [SerializeField] private Sprite _playerUnhealtySprite;

    [Header("Settings")]
    [SerializeField] private float _scaleDuration;

    private RectTransform[] _playerHealthTransform;

    private void Awake()
    {
        _playerHealthTransform = new RectTransform[_playerHealthImages.Length];

        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            _playerHealthTransform[i] = _playerHealthImages[i].gameObject.GetComponent<RectTransform>();
        }
    }
    public void AnimateDamage()
    {
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            if (_playerHealthImages[i].sprite == _playerHealtySprite)
            {
                AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransform[i]);
                break;
            }
        }
    }
    //for Testing

    private void Update() {
        if(Input.GetKeyDown(KeyCode.O))
        {
            AnimateDamage();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            AnimateDamageForAll();
        }
    }
    public void AnimateDamageForAll()
    {
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransform[i]);             
        }
    }

    private void AnimateDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        activeImageTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = _playerUnhealtySprite;
            activeImageTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }

}
