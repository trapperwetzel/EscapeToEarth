// Written by Manav Mendonca
// Updated on 12/03/2024

using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public float attackDamage = 10f;

    private float nextAttackTime = 0f;
    private Transform player;
    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log($"Player found: {player.name}");
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            agent.isStopped = true;
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
        Debug.Log($"{gameObject.name} is attacking the player."); // check if damage is being dealt
    }
}
