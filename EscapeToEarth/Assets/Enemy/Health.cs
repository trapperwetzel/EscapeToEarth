// Written by Manav Mendonca
// Updated on 12/03/2024
//Updated by Julian Van Beusekom 12/4/24

using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isTakingDamage = false;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"{gameObject.name} initialized with health: {currentHealth}");
    }

    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0) return; // Prevent logic for already dead entities

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        Animator animator = GetComponent<Animator>();
        if (animator != null && !isTakingDamage)
        {
            isTakingDamage = true;
            animator.SetTrigger("TakeDamage");
            StartCoroutine(ResetDamageState());
        }
    }

    private IEnumerator ResetDamageState()
    {
        yield return new WaitForSeconds(1f);
        isTakingDamage = false;
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} triggered Die()");
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject, 2f); // Delay destruction to allow animation playback
    }
}
