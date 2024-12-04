using UnityEngine;
using UnityEngine.UI;

// Coded by Julian Van Beusekom
// 12/4/24

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // Slider variable
    public Health healthComponent; // Reference to the Health script

    void Start()
    {
        // Initialize the health bar slider with the max health
        if (healthComponent != null)
        {
            healthSlider.maxValue = healthComponent.maxHealth;
            healthSlider.value = healthComponent.currentHealth;
        }
    }

    void Update()
    {
        // Update the slider value based on current health
        if (healthComponent != null && healthSlider.value != healthComponent.currentHealth)
        {
            healthSlider.value = healthComponent.currentHealth;
        }
    }
}

