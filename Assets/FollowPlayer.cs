using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public Vector3 offset = new Vector3(0, 0, -10); // Offset from the player position

    void Update()
    {
        if (playerTransform != null)
        {
            // Make the particle system follow the player
            transform.position = playerTransform.position + offset;
        }
    }
}
