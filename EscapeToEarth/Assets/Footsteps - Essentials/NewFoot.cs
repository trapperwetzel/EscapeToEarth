using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public AudioSource footstepAudio;
    public float stepInterval = 0.9f; // Time between steps in seconds
    private float nextStepTime = 0.2f;



    private ThirdPersonMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        if (footstepAudio == null)
        {
            footstepAudio = GetComponent<AudioSource>();
        }

        if (footstepAudio != null)
        {
            footstepAudio.Stop(); // Ensure sound isn't playing initially
        }
        // Find and assign the ThirdPersonMovement script
        movement = GetComponent<ThirdPersonMovement>();

        if (movement == null)
        {
            Debug.LogError("ThirdPersonMovement script not found on the GameObject!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Make it so up down keys work
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Time.time >= nextStepTime) // Check if enough time has passed
            {
                StartFootsteps();
                nextStepTime = Time.time + stepInterval; // Schedule next step
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            StopFootsteps();
        }

        if (movement != null)
        {
            if (movement.sprinting)
            {
                stepInterval = 0.15f; // Shorter interval for sprinting
            }
            else
            {
                stepInterval = 0.9f; // Default interval
            }
        }

    }

    void StartFootsteps()
    {
        if (!footstepAudio.isPlaying)
        {
            footstepAudio.Play();
        }
    }

    void StopFootsteps()
    {
        footstepAudio.Stop();
    }
}
