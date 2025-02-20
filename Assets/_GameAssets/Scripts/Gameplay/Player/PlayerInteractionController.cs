using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Transform _playerVİsualTransform;
    private PlayerController _playerController;
    private Rigidbody _playerRigidbody;
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IBoostable>(out var boostable))
        {
            boostable.Boost(_playerController);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GiveDamage(_playerRigidbody, _playerVİsualTransform);
            CameraShake.Instance.ShakeCamera(1.5f,0.5f);
        }
    }
}
