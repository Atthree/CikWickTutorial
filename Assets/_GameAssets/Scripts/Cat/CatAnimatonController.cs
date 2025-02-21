using UnityEngine;

public class CatAnimatonController : MonoBehaviour
{
    [SerializeField] private Animator _catAnimator;

    private CatStateController _catStateController;

    private void Awake()
    {
        _catStateController = GetComponent<CatStateController>();
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play 
            && GameManager.Instance.GetCurrentGameState() != GameState.Resume
            && GameManager.Instance.GetCurrentGameState() != GameState.CutScene
            && GameManager.Instance.GetCurrentGameState() != GameState.GameOver)
        {
            _catAnimator.enabled = false;
            return;
        }
        SetCatAnimations();
    }

    private void SetCatAnimations()
    {
        _catAnimator.enabled = true;
        var currentCatState = _catStateController.GetCurrentState();
        switch(currentCatState)
        {
            case CatState.Idle:
                _catAnimator.SetBool(Const.CatAnimations.IS_IDLING,true);
                _catAnimator.SetBool(Const.CatAnimations.IS_WALKİNG,false);
                _catAnimator.SetBool(Const.CatAnimations.IS_RUNNING,false);
                break;
            case CatState.Walking:
                _catAnimator.SetBool(Const.CatAnimations.IS_IDLING,false);
                _catAnimator.SetBool(Const.CatAnimations.IS_WALKİNG,true);
                _catAnimator.SetBool(Const.CatAnimations.IS_RUNNING,false);
                break;
            case CatState.Running:
                _catAnimator.SetBool(Const.CatAnimations.IS_RUNNING,true);
                break;
            case CatState.Attacking:
                _catAnimator.SetBool(Const.CatAnimations.IS_ATTACKING,true);
                break;
        }
    }
}
