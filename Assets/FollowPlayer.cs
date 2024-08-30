using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public Vector3 offset = new Vector3(0, 0, -10); // Offset from the player position
    [SerializeField]
    public float minY = 50f; // Minimum y-value for activation
    public float maxY = 100f; // Maximum y-value for activation

    private ParticleSystem particleSystem; // Reference to the ParticleSystem component

    void Start()
    {
        // Get the ParticleSystem component attached to this GameObject
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Update the position of the GameObject to follow the player
            transform.position = playerTransform.position + offset;

            // Check if the player's y-position is within the specified range
            if (playerTransform.position.y >= minY && playerTransform.position.y <= maxY)
            {
                if (!particleSystem.isPlaying)
                {
                    particleSystem.Play(); // Activate the particle system
                }
            }
            else
            {
                if (particleSystem.isPlaying)
                {
                    particleSystem.Stop(); // Deactivate the particle system
                }
            }
        }
    }

}
