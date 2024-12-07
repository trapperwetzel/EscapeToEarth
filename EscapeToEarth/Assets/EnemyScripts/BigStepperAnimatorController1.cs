// Written by Manav Mendonca
// Updated on 12/03/2024

using UnityEngine;
using UnityEngine.AI;

public class BigStepperAnimatorController : MonoBehaviour
{
    // References
    private Animator animator;
    private NavMeshAgent navAgent;

    // Parameters
    public float speedThreshold = 0.1f; // Threshold for walking
    public float runningSpeed = 2.0f;   // Speed for running

    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Update speed parameter
        float speed = navAgent != null ? navAgent.velocity.magnitude : 0f;
        animator.SetFloat("Speed", speed);
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void TriggerTakeDamage()
    {
        animator.SetTrigger("TakeDamage");
    }

    public void TriggerDeath()
    {
        animator.SetTrigger("Die");
    }
}
