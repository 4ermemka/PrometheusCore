using System;

public class HealthBar
{
    public Action<int,int,int> OnDeath;

    public int MaxHealth;
    public int Health;
    public int Radiation;

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
}
