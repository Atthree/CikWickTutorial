using UnityEngine;

public class CatStateController : MonoBehaviour
{
    [SerializeField] private CatState _currentGetState = CatState.Walking;

    private void Start() {
        ChangeState(CatState.Walking);
    }
    public void ChangeState(CatState newState){
        if(_currentGetState == newState){return;}
        _currentGetState = newState;
    }

    public CatState GetCurrentState(){
        return _currentGetState;
    }
}
