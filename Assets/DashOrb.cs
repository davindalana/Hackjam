using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashOrb : MonoBehaviour
{
    public float respawnTime = 5f; // Time in seconds before the orb respawns
    private SpriteRenderer orbRenderer; // Assuming you're using a SpriteRenderer for a 2D game
    private Collider2D orbCollider;
    public ParticleSystem orbParticles; // Reference to the particle system
    public float idleMoveDistance = 0.5f; // Distance to move up and down
    public float idleMoveSpeed = 2f; // Speed of the up and down movement

    private Vector3 originalPosition;
    private Color originalColor;
    private bool isIdle = true;

    public float bounceDuration = 0.3f; // Duration of the bounce effect
    public float bounceScaleFactor = 1.1f;
    private Vector3 originalScale;

    private void Start()
    {
        orbRenderer = GetComponent<SpriteRenderer>(); // Assuming a SpriteRenderer for 2D
        orbCollider = GetComponent<Collider2D>();

        originalColor = orbRenderer.color; // Store the original color
        originalPosition = transform.position; // Store the original position
        originalScale = transform.localScale;

        // Start the idle animation and particles
        StartCoroutine(IdleMovement());
        if (orbParticles != null)
        {
            orbParticles.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.RefillDashes(); // Refill the player's dashes
                transform.position = originalPosition;
                StartCoroutine(RespawnOrb()); // Start the respawn process
            }
        }
    }

    private IEnumerator IdleMovement()
    {
        while (isIdle)
        {
            // Move the orb up and down over time
            float newY = originalPosition.y + Mathf.Sin(Time.time * idleMoveSpeed) * idleMoveDistance;
            transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);

            yield return null; // Wait until the next frame
        }
    }

    private IEnumerator RespawnOrb()
    {
        // Make the orb semi-transparent and disable the collider and particles
        orbRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.1f); // 10% visible
        orbCollider.enabled = false;
        isIdle = false; // Stop the idle movement

        // Stop the particles
        if (orbParticles != null)
        {
            orbParticles.Stop();
        }

        // Wait for the respawn time
        yield return new WaitForSeconds(respawnTime);

        // Revert to the original color and re-enable the collider
        orbRenderer.color = originalColor;
        orbCollider.enabled = true;

        // Restart the idle animation and particles
        isIdle = true;
        StartCoroutine(IdleMovement());
        if (orbParticles != null)
        {
            orbParticles.Play();
            var emission = orbParticles.emission;
            var mainModule = orbParticles.main;
            mainModule.startSpeed = new ParticleSystem.MinMaxCurve(3, 3);
            orbParticles.Emit(100); // Adjust the count for a larger burst
            mainModule.startSpeed = new ParticleSystem.MinMaxCurve(1, 2);
            yield return StartCoroutine(PlayBounceEffect());
        }
    }
    private IEnumerator PlayBounceEffect()
    {
        float elapsedTime = 0f;

        while (elapsedTime < bounceDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / bounceDuration;

            // Apply a bouncing scale effect using a sin function for smoothness
            float scale = Mathf.Lerp(1f, bounceScaleFactor, Mathf.Sin(t * Mathf.PI));

            transform.localScale = originalScale * scale;

            yield return null;
        }

        // Ensure the scale returns to the original size
        transform.localScale = originalScale;
    }
}
