using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Written by Trapper Wetzel
// Final Edit 12/08/2024
public class ThirdPersonMovement : MonoBehaviour
{
    public Transform cam; // Assign the camera Transform in the Inspector

    CharacterController controller;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Animator anim;

    Vector2 movement;
    public float walkSpeed;
    public float sprintSpeed;
    public bool sprinting;
    float trueSpeed;

    // Jumping
    public float jumpHeight;
    public float gravity;
    public bool isGrounded;
    Vector3 velocity;

    // Attacking
    public bool isAttacking = false;
    public float attackDamage = 20f; // Damage dealt by the sword
    public float attackRange = 2f;  // Attack range
    public Transform attackPoint; // The point where the attack occurs (usually the sword tip or hand)

    // Stored direction before attack
    private float lastRotationY;

    void Start()
    {
        trueSpeed = walkSpeed;
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Ground Check
        isGrounded = controller.isGrounded;
        anim.SetBool("IsGrounded", isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        // Sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            trueSpeed = sprintSpeed;
            sprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            trueSpeed = walkSpeed;
            sprinting = false;
        }

        // Movement Input
        if (!isAttacking) // Prevent rotation while attacking
        {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;

            if (direction.magnitude >= 0.1f)
            {
                // Calculate target angle relative to camera
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

                // Smoothly rotate character toward target angle
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // Move character in the direction of camera's forward
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * trueSpeed * Time.deltaTime);

                // Update Animator based on speed
                anim.SetFloat("Speed", sprinting ? 2 : 1); // 2 for Sprinting, 1 for Walking
            }
            else
            {
                anim.SetFloat("Speed", 0); // Idle
            }
        }
        else
        {
            // Lock rotation during attack to prevent automatic rotation with animation
            transform.rotation = Quaternion.Euler(0f, lastRotationY, 0f);
        }

        // Sync Character with Camera Forward Direction
        if (movement.magnitude < 0.1f)
        {
            transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity when not grounded
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Apply vertical velocity to character
        controller.Move(velocity * Time.deltaTime);

        // Handle Attack
        if (Input.GetButtonDown("Fire1") && !isAttacking) // Prevent multiple attacks at once
        {
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {
        isAttacking = true;

        // Save current rotation to avoid automatic model rotation during the attack animation
        lastRotationY = transform.eulerAngles.y;

        // Temporarily disable gravity during the attack
        float originalGravity = gravity;
        gravity = 0;

        anim.SetTrigger("SwordSlash"); // Trigger the sword slash animation

        // Wait for the attack animation to finish 
        yield return new WaitForSeconds(0.5f); // Adjust to match the desired attack speed.

        // Detect enemies in range of the attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Health enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage); // Apply damage to enemy
                    Debug.Log("Enemy hit!");
                }
            }
        }

        gravity = originalGravity;  // Restore gravity

        // Allow movement and other actions again
        isAttacking = false;

        // Return to idle or movement after attack
        anim.SetFloat("Speed", 0); // Transition to idle or movement
    }

    // Visualize attack range (optional, for debugging)
    private void OnDrawGizmos()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); // Show attack range
    }
}

