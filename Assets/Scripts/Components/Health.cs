using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    public delegate void HealthChangedHandler(int currentHealth, int maxHealth);
    public event HealthChangedHandler OnHealthChanged;

    public delegate void HealthDeletedHandler();
    public event HealthDeletedHandler OnHealthDepleted;

    public delegate void HeathHealedHandler(int amountHealed);
    public event HeathHealedHandler OnHealed;

    public delegate void HealthDamagedHandler(int amountDamaged);
    public event HealthDamagedHandler OnDamaged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            HandleHealthIsDepleted();
        }
        OnDamaged?.Invoke(damage);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnHealed?.Invoke(amount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void DecreaseMaxHealth(int amount)
    {
        maxHealth -= amount;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private void HandleHealthIsDepleted()
    {
        Debug.Log($"{gameObject.name} has died.");
        OnHealthDepleted?.Invoke();
    }
}