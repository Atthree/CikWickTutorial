using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour,ICollectible
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _forceIncrase;
    [SerializeField] private float _resetBoostDuration;

    public void Collect()
    {
        _playerController.SetJumpForce(_forceIncrase,_resetBoostDuration);
        Destroy(gameObject);
    }
}
