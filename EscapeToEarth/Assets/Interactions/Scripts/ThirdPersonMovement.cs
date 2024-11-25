using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    bool sprinting;
    float trueSpeed;

    // Jumping
    public float jumpHeight;
    public float gravity;
    public bool isGrounded;
    Vector3 velocity;

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

        // Reset Animator's Transform (prevents offset issues)
       // anim.transform.localPosition = Vector3.zero;
        //anim.transform.localEulerAngles = Vector3.zero;

        // Movement Input
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
    }
}
