using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private bool _canBeDamaged = true;
    [Space]
    [Header("Debug")]
    [SerializeField] private int _currentHealth;

    public float CurrentHealthPercent => (float)_currentHealth / (float)_maxHealth;
    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public bool IsDead { get; private set; }

    public bool CanBeDamaged
    {
        get => _canBeDamaged;

        set => _canBeDamaged = value;
    }

    public event Action OnHealthChanged;
    public event Action OnRevive;
    public event Action OnDeath;

    public event Action<DamageInfo> OnDamaged;
    public event Action<int> OnHealed;

    //temp
    private void Awake()
    {
        ResetHealth();
    }

    public void SetMaxHealth(int maxHealth, bool resetHealth = false)
    {
        _maxHealth = maxHealth;

        if (resetHealth)
        {
            ResetHealth();
        }
    }

    public void Revive(int healCount)
    {
        IsDead = false;
        Heal(healCount);

        OnRevive?.Invoke();
    }

    public void Revive()
    {
        IsDead = false;
        HealToMax();

        OnRevive?.Invoke();
    }

    public void ApplyDamage(DamageInfo dmgInfo)
    {
        _currentHealth -= dmgInfo.DamageCount;

        ClampHealth();

        if(_currentHealth <= 0)
        {
            IsDead = true;

            OnDeath?.Invoke();
        }

        OnDamaged?.Invoke(dmgInfo);
        OnHealthChanged?.Invoke();
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        
        OnHealthChanged?.Invoke();
    }

    public void Heal(int healAmount)
    {
        if(_currentHealth < _maxHealth)
        {
            _currentHealth += healAmount;
            ClampHealth();

            OnHealthChanged?.Invoke();
            OnHealed?.Invoke(healAmount);
        }
    }

    public void HealToMax()
    {
        Heal(_maxHealth - _currentHealth);
    }

    private void ClampHealth()
    {
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }

#if UNITY_EDITOR

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            AddDamage();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            AddHeal();
        }
    }
    */

    [ContextMenu("Add Damage")]
    public void AddDamage()
    {
        ApplyDamage(new DamageInfo(null, 10));
    }

    [ContextMenu("Add Heal")]
    public void AddHeal()
    {
        Heal(10);
    }

#endif
}
