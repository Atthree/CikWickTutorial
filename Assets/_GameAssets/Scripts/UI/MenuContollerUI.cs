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
            AudioManager.Instance.Play(SoundType.TransitionSound);
            TransitionManager.Instance.LoadLevel(Const.SceneNames.GAME_SCENE);
        });
        _quittButton.onClick.AddListener(()=>
        {
            AudioManager.Instance.Play(SoundType.ButtonClickSound);
            Debug.Log("Quitting Game");
            Application.Quit();
        });
    }
}
