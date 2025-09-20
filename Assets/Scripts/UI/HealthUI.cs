using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private TMP_Text healthText;

    private void OnEnable()
    {
        playerHealth.OnHealed += HandleHealed;
        playerHealth.OnDamaged += HandleDamaged;
        playerHealth.OnHealthChanged += HandleHealthChanged;
        UpdateHealthText(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }

    private void UpdateHealthText(int currentHealth, int maximumHealth)
    {
        healthText.text = $"{currentHealth} / {maximumHealth}";
    }

    private void OnDisable()
    {
        playerHealth.OnHealed -= HandleHealed;
        playerHealth.OnDamaged -= HandleDamaged;
        playerHealth.OnHealthChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(int currentHealth, int maximumHealth)
    {
        UpdateHealthText(currentHealth, maximumHealth);
    }
         
    private void HandleHealed(int amount)
    {
        // TODO
    }

    private void HandleDamaged(int amount)
    {
        // TODO
    }
}
