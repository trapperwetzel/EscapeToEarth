using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Transform cam;

    CharacterController controller;
    float turnSmoothTime = .1f;
    float turnSmoothVelocity;

    Vector2 movement;
    public float walkSpeed;
    public float sprintSpeed;
    float trueSpeed;

    //Jumping

    public float jumpHeight;
    public float gravity;
    private bool isGrounded;
    Vector3 velocity;
    
    void Start()
    {
        trueSpeed = walkSpeed;
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            trueSpeed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            trueSpeed = walkSpeed;
        }
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;

        

     

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * trueSpeed * Time.deltaTime);
        }
        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Calculate jump velocity
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity only when not grounded
        if (isGrounded == false)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Apply the velocity vector to the controller
        controller.Move(velocity * Time.deltaTime);
    }
}

