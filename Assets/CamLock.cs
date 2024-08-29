using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLock : MonoBehaviour
{
    [Header("Bounds Settings")]
    public float leftBound = -5f;  // Left boundary limit
    public float rightBound = 5f;  // Right boundary limit
    public float topBound = 3f;  // Top boundary limit
    public float bottomBound = -3f;  // Bottom boundary limit
    public float Xoffset;
    public float Yoffset;
    public Transform player;  // Reference to the player transform

    void Update()
    {
        // Follow the player's position
        float targetXPosition = player.position.x+Xoffset;
        float targetYPosition = player.position.y+Yoffset;

        // Clamp the target X position between the left and right bounds
        targetXPosition = Mathf.Clamp(targetXPosition, leftBound, rightBound);

        // Clamp the target Y position between the top and bottom bounds
        targetYPosition = Mathf.Clamp(targetYPosition, bottomBound, topBound);

        // Update the object's position, locking it within the specified bounds
        transform.position = new Vector3(targetXPosition, targetYPosition, transform.position.z);
    }
}
