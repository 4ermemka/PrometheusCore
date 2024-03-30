using Newtonsoft.Json;
using System;

public class HealthBar
{
    //<int,int,int> == <MaxHealth, Health, Radiation>
    [JsonIgnore]
    public Action<int,int,int> OnDeath;
    [JsonIgnore]
    public Action<int, int> OnHeal;
    [JsonIgnore]
    public Action<int, int> OnDamage;
    [JsonIgnore]
    public Action<int, int> OnRadiationIncreased;
    [JsonIgnore]
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
    public HealthBar()
    {
        MaxHealth = 16;
        Health = MaxHealth;
        Radiation = new Radiation();
        SubscribeToInner();
    }

    public HealthBar(int maxHealth)
    {
        MaxHealth = maxHealth;
        Health = MaxHealth;
        Radiation = new Radiation();
        SubscribeToInner();
    }

    public HealthBar(int maxHealth, int health, Radiation radiation)
    {
        MaxHealth = maxHealth;
        Health = health;
        Radiation = radiation;
        SubscribeToInner();
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
        Radiation.UpdateState(updatedState.Radiation);
    }

    private void SubscribeToInner()
    { 
        Radiation.OnRadiationIncreased += OnRadiationIncreased;
        Radiation.OnRadiationDecreased += OnRadiationDecreased;
        Radiation.OnRadiationDamage += new Action(() =>
        {
            Health--;
        });
    }
}
