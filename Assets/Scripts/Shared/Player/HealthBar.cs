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
    private int _radiation;

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
    public int Health { get; set; }
    public int Radiation { get; set; }

    public HealthBar(int maxHealth)
    {
        MaxHealth = maxHealth;
        Health = MaxHealth;
        Radiation = 0;
    }

    public HealthBar(int maxHealth, int health, int radiation = 0)
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
        if(Health < Radiation)
        {
            OnDeath?.Invoke(MaxHealth, Health, Radiation);
        }
    }

    public void TakeRad(int radiation)
    {
        Radiation += radiation;
        if(Health < Radiation)
        {
            OnDeath?.Invoke(MaxHealth, Health, Radiation);
        }
    }

    public void HealRad(int radiation)
    {
        Radiation -= radiation;
        if(Radiation < 0)
            Radiation = 0;
    }

    public void UpdateState(HealthBar updatedState)
    { 
        
    }
}
