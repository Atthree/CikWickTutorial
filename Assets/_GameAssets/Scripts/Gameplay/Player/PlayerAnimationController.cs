using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    private PlayerController _playerController;
    private StateController _stateController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _stateController = GetComponent<StateController>();
    }
    private void Start()
    {
        _playerController.OnPlayerJump += PlayerController_OnPlayerJumped;
    }

    private void PlayerController_OnPlayerJumped()
    {
        _playerAnimator.SetBool(Const.PlayerAnimations.IS_JUMPİNG,true);
        Invoke(nameof(ResetJumping),0.5f);
    }
    private void ResetJumping(){
        _playerAnimator.SetBool(Const.PlayerAnimations.IS_JUMPİNG,false);
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play 
            && GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }
        SetPlayerAnimations();
    }

    private void SetPlayerAnimations()
    {
        var currentState = _stateController.GetCurrentState();
        switch (currentState)
        {
            case PlayerState.Idle:
                _playerAnimator.SetBool(Const.PlayerAnimations.IS_SLIDING, false);
                _playerAnimator.SetBool(Const.PlayerAnimations.IS_MOVİNG, false);
                break;
            case PlayerState.Move:
                _playerAnimator.SetBool(Const.PlayerAnimations.IS_SLIDING, false);
                _playerAnimator.SetBool(Const.PlayerAnimations.IS_MOVİNG, true);
                break;
            case PlayerState.SlideIdle:
                _playerAnimator.SetBool(Const.PlayerAnimations.IS_SLIDING, true);
                _playerAnimator.SetBool(Const.PlayerAnimations.IS_SLIDING_ACTIVE, false);
                break;
            case PlayerState.Slide:
                _playerAnimator.SetBool(Const.PlayerAnimations.IS_SLIDING, true);
                _playerAnimator.SetBool(Const.PlayerAnimations.IS_SLIDING_ACTIVE, true);
                break;
        }
    }
}
