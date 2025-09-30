using System;
using UnityEngine;


public class Health : MonoBehaviour
{
    [Header("Health Properties")]
    [Tooltip("Total amount of health")]
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _health;

    public float Cur { get { return _health; } }

    private bool _isDead;
    public bool IsDead { get { return _isDead; } }

    public float HealthRatio { get { return _health / _maxHealth; } }

    public event EventHandler<HealthChangedEventArgs> EntityDamaged;
    public event EventHandler<HealthChangedEventArgs> EntityHealed;
    public event Action EntityDied;
    public event Action EntityFullHealed;

    protected virtual void OnHealed(float prev, float cur) => EntityHealed?.Invoke(this, new HealthChangedEventArgs(prev, cur));
    protected virtual void OnDamaged(float prev, float cur) => EntityDamaged?.Invoke(this, new HealthChangedEventArgs(prev, cur));
    protected virtual void OnDead() => EntityDied?.Invoke();
    protected void OnFullHealed() => EntityFullHealed?.Invoke();
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _health = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log($"{gameObject.name}: health => {_health}");
    }

    public void Heal(float amountToHeal)
    {
        if (_health == _maxHealth) return;

        float prevHealth = _health;
        _health += amountToHeal;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        if (_health == _maxHealth)
        {
            OnFullHealed();
        }
        OnHealed(prevHealth, _health);
    }

    public void Damage(float amountToDamage)
    {
        if (_isDead) return;

        float prevHealth = _health;
        _health -= amountToDamage;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        OnDamaged(prevHealth, _health);

        if (_health <= 0)
        {
            _isDead = true;
            OnDead();
        }
    }
}

public class HealthChangedEventArgs : EventArgs
{
    public float Prev { get; }
    public float Cur { get; }
    public float Dela { get { return Cur - Prev; } }

    public HealthChangedEventArgs(float prev, float cur)
    {
        Prev = prev;
        Cur = cur;
    }
}