using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnTouch : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has entered the collider
        if (other.CompareTag("Player"))
        {
            // Play the audio
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player has exited the collider
        if (other.CompareTag("Player"))
        {
            // Stop the audio
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}