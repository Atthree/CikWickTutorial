using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBoostable
{
    [Header("References")]
    [SerializeField] private Animator _spatulaAnimator;
    [Header("Settings")]
    [SerializeField] private float _jumpForce;
    private bool _isActivated;
    public void Boost(PlayerController playerController)
    {
        if (_isActivated) { return; }
        PlayBoosterAnimations();
        Rigidbody playerRigidbody = playerController.GetPlayerRigidbody();
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
        playerRigidbody.AddForce(transform.forward * _jumpForce, ForceMode.Impulse);
        _isActivated = true;
        Invoke(nameof(ResetActivated), 0.2f);
        AudioManager.Instance.Play(SoundType.SpatulaSound);
    }

    private void PlayBoosterAnimations()
    {
        _spatulaAnimator.SetTrigger(Const.OtherAnimations.IS_SPATULA_JUMPING);
    }
    private void ResetActivated()
    {
        _isActivated = false;
    }
}
