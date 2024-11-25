using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public float attackDamage = 10f;

    private float nextAttackTime = 0f;
    private Transform player;
    private Animator animator;

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the player is in range
        if (player != null && Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            // Trigger an attack if cooldown has passed
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void Attack()
    {
        // Play attack animation
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Damage the player
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
