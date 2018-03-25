using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    [SyncVar]
    public float CurrentHealth = 100f;

    [SyncVar]
    public float MaxHealth = 100f;

    public virtual bool IsDead
    {
        get
        {
            return CurrentHealth <= 0f;
        }
    }

    public virtual bool FullHealth
    {
        get
        {
            return CurrentHealth == MaxHealth;
        }
    }

    [Server]
    public void SetMaxHealth()
    {
        if(CurrentHealth < MaxHealth)
            CurrentHealth = MaxHealth;
    }

    [Server]
    public void ChangeHealth(float change)
    {
        SetHealth(CurrentHealth + change);
    }

    [Server]
    public void SetHealth(float health)
    {
        // Cannot go below 0 or above max.
        CurrentHealth = Mathf.Clamp(health, 0f, MaxHealth);
    }
}