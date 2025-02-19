using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private EggCounterUI _eggCounterUI;

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;

    private GameState _currentGameState;
    private int _currentEggCount;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        ChangeGameState(GameState.Play);
    }
    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;
        Debug.Log("Game State: " + gameState);
    }
    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounterUI.SetEggCounter(_currentEggCount, _maxEggCount);
        if (_currentEggCount == _maxEggCount)
        {
            Debug.Log("Game Win");
            _eggCounterUI.SetEggCompleted();
        }
        Debug.Log("Egg Count" + _currentEggCount);
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}
