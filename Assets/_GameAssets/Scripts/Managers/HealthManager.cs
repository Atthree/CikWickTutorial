using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }
    public event Action OnPlayerDeath;
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private PlayerHealthUI _playerHealthUI;
    private int _currentHealth;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damageAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damageAmount;
            _playerHealthUI.AnimateDamage();

            if (_currentHealth <= 0)
            {
                OnPlayerDeath?.Invoke();
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth = Mathf.Min(_currentHealth + healAmount, _maxHealth);
        }
    }
}
