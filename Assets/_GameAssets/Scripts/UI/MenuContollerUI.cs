using MaskTransitions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuContollerUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quittButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(() => 
        {
            TransitionManager.Instance.LoadLevel(Const.SceneNames.GAME_SCENE);
        });
        _quittButton.onClick.AddListener(()=>
        {
            Debug.Log("Quitting Game");
            Application.Quit();
        });
    }
}
