using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip landSound;
    private AudioSource audioSource;
    private bool isJumping = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Play jump sound when space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            PlaySound(jumpSound);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player lands and play land sound
        if (isJumping && collision.contacts.Length > 0)
        {
            isJumping = false;
            PlaySound(landSound);
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

