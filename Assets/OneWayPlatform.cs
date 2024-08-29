using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    public Collider2D platformCollider; // The collider for the platform itself
    public Collider2D triggerCollider;  // The collider used to detect when the player is below the platform

    private void OnTriggerEnter2D(Collider2D other)
    {
     
                // Allow the player to pass through by making the platform a trigger
                platformCollider.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // When the player exits, ensure the platform is solid
            platformCollider.isTrigger = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // If the player is above the platform, make it solid
            if (other.transform.position.y > platformCollider.bounds.center.y)
            {
                platformCollider.isTrigger = false;
            }
            else
            {
                // If the player is below, allow them to pass through
                platformCollider.isTrigger = true;
            }
        }
    }
}
