using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashOrb : MonoBehaviour
{
    public float respawnTime = 5f; // Time in seconds before the orb respawns
    private SpriteRenderer orbRenderer; // Assuming you're using a SpriteRenderer for a 2D game
    private Collider2D orbCollider;

    private Color originalColor;

    private void Start()
    {
        orbRenderer = GetComponent<SpriteRenderer>(); // Assuming a SpriteRenderer for 2D
        orbCollider = GetComponent<Collider2D>();

        originalColor = orbRenderer.color; // Store the original color
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.RefillDashes(); // Refill the player's dashes
                StartCoroutine(RespawnOrb()); // Start the respawn process
            }
        }
    }

    private IEnumerator RespawnOrb()
    {
        // Make the orb semi-transparent and disable the collider
        orbRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.1f); // 10% visible
        orbCollider.enabled = false;

        // Wait for the respawn time
        yield return new WaitForSeconds(respawnTime);

        // Revert to the original color and re-enable the collider
        orbRenderer.color = originalColor;
        orbCollider.enabled = true;
    }
}
