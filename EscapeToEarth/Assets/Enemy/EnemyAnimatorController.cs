using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator animator;

    public float speed; // Movement speed to control transitions
    public bool isAttacking; // Set to true to trigger attack
    public bool isExiting; // Set to true to trigger exit

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Update Speed parameter
        animator.SetFloat("Speed", speed);

        // Trigger attack animation
        if (isAttacking)
        {
            animator.SetTrigger("Attack");
            isAttacking = false; // Reset trigger
        }

        // Trigger exit state
        animator.SetBool("IsExiting", isExiting);
    }
}

