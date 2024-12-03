using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip walkSound;
    public AudioClip runSound;
    private AudioSource audioSource;
    private CharacterController controller;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded && controller.velocity.magnitude > 0.2f)
        {
            if (Input.GetKey(KeyCode.LeftShift)) // Running
            {
                PlaySound(runSound, 0.4f); // Faster steps
            }
            else // Walking
            {
                PlaySound(walkSound, 0.7f); // Slower steps
            }
        }
    }

    void PlaySound(AudioClip clip, float stepInterval)
    {
        if (audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.Play();
            Invoke(nameof(StopSound), stepInterval);
        }
    }

    void StopSound()
    {
        audioSource.Stop();
    }
}

