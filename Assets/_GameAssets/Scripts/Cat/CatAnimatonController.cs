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
        SetCatAnimations();
    }

    private void SetCatAnimations()
    {
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
