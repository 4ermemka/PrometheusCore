using System;

public class HealthBar
{
    //<int,int,int> == <MaxHealth, Health, Radiation>
    public Action<int,int,int> OnDeath;
    public Action<int, int> OnHeal;
    public Action<int, int> OnDamage;
    public Action<int, int> OnRadiationIncreased;
    public Action<int, int> OnRadiationDecreased;

    private int _maxHealth;
    private int _health;
    private Radiation _radiation;

    public Radiation Radiation 
    {
        get => _radiation;
        set
        { 
            _radiation = value;
        }
    }

    public int MaxHealth 
    {
        get
        { 
            return _maxHealth;
        }
        set
        { 
            _maxHealth = value;
        }
    }
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            if(value > _health)
                OnHeal?.Invoke(value-_health, Health);
            else if (value < _health)
                OnDamage?.Invoke(_health-value, Health);
        }
    }

    public HealthBar(int maxHealth)
    {
        MaxHealth = maxHealth;
        Health = MaxHealth;
        Radiation = new Radiation();
    }

    public HealthBar(int maxHealth, int health, Radiation radiation)
    {
        MaxHealth = maxHealth;
        Health = health;
        Radiation = radiation;
    }

    public void Heal()
    {
        Health = MaxHealth;
    }

    public void Heal(int healAmount)
    {
        Health += healAmount;
        if (Health > MaxHealth)
            Health = MaxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;
        if(Health < 0)
        {
            OnDeath?.Invoke(MaxHealth, Health, Radiation.RadiationLevel);
        }
    }

    public void UpdateState(HealthBar updatedState)
    { 
        _maxHealth = updatedState.MaxHealth;
        _health = updatedState.Health;
        Radiation = updatedState.Radiation;
    }

    public override string ToString()
    {
        string result = $"{_health}|{_maxHealth}|{Radiation.ToString()}";
        return result ;
    }
}
