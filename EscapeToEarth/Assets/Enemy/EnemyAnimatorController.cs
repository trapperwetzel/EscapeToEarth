using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    // References
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent navAgent; // Assuming the enemy uses a NavMeshAgent for movement

    // Parameters
    public float speedThreshold = 0.1f; // Threshold to determine if the enemy is walking
    public float runningSpeed = 2.0f;   // Speed at which the enemy is considered running
    private bool isFalling = false;     // Whether the enemy is falling
    private bool isAttacking = false;   // Whether the enemy is attacking

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();

        // Get the NavMeshAgent component if the enemy uses NavMesh for movement
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        // Update speed parameter
        float speed = navAgent != null ? navAgent.velocity.magnitude : 0f;
        animator.SetFloat("Speed", speed);

        // Update falling state
        animator.SetBool("IsFalling", isFalling);

        // Trigger attack
        if (isAttacking)
        {
            animator.SetTrigger("Attack");
            isAttacking = false; // Reset the attack trigger
        }
    }

    // Example method to trigger a fall
    public void StartFalling()
    {
        isFalling = true;
        animator.SetBool("IsFalling", true);
    }

    // Example method to stop falling (e.g., when landing)
    public void StopFalling()
    {
        isFalling = false;
        animator.SetBool("IsFalling", false);
    }

    // Example method to trigger an attack
    public void TriggerAttack()
    {
        isAttacking = true;
    }

    public void DealDamage(GameObject player)
    {
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(10f); // Example damage value
        }
    }

    void CheckFalling()
    {
        // Raycast downward to check if the enemy is airborne
        if (!Physics.Raycast(transform.position, Vector3.down, 1.0f))
        {
            StartFalling();
        }
        else
        {
            StopFalling();
        }
    }

    private float attackCooldown = 2.0f;
    private float nextAttackTime = 0.0f;




}
