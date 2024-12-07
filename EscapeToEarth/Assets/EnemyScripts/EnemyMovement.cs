// Written by Manav Mendonca
// Updated on 12/03/2024

using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float chaseRange = 10f;

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= agent.stoppingDistance)
        {
            agent.ResetPath(); // Stop moving
            animator.SetFloat("Speed", 0f); // Idle state
        }
        else if (distanceToPlayer <= chaseRange)
        {
            agent.SetDestination(player.position);
            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);
        }
        else
        {
            agent.ResetPath();
            animator.SetFloat("Speed", 0f); // Idle state
        }
    }
}
