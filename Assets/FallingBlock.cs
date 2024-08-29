using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    [Header("Falling Settings")]
    public float fallDelay = 0.5f; // Time before the block starts falling
    public float resetDelay = 2f;  // Time before the block resets to its original position
    public bool destroyOnFall = false; // Should the block be destroyed after falling?

    [Header("Shake Settings")]
    public float shakeDuration = 0.5f; // Duration of the shake effect before falling
    public float shakeMagnitude = 0.05f; // Magnitude of the shake effect

    private Vector3 originalPosition; // The original position of the block
    private Rigidbody2D rb;
    private bool isFalling = false;
    public CircleCollider2D specificCollider;

    void Start()
    {
        // Store the original position of the block
        originalPosition = transform.position;

        // Get the Rigidbody2D component attached to this block
        rb = GetComponent<Rigidbody2D>();

        // Ensure the Rigidbody2D is set to Kinematic at the start
        rb.isKinematic = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collided with the block
        if (other.CompareTag("Player") && !isFalling)
        {
            if (specificCollider != null && other.IsTouching(specificCollider))
            {
                // Start the shake and fall sequence
                StartCoroutine(ShakeAndFall());
            }
        }
    }

    private IEnumerator ShakeAndFall()
    {
        specificCollider.enabled = false;
        isFalling = true;

        // Start the shake effect
        yield return StartCoroutine(Shake(shakeDuration));

        // Wait for the fall delay (if any)
        yield return new WaitForSeconds(fallDelay);

        // Set the Rigidbody2D to Dynamic to make the block fall
        rb.isKinematic = false;

        // Optionally destroy the block after a delay
        if (destroyOnFall)
        {
            yield return new WaitForSeconds(resetDelay);
            Destroy(gameObject);
        }
        else
        {
            // Wait for a while before resetting the block
            yield return new WaitForSeconds(resetDelay);
            ResetBlock();
        }
    }

    private IEnumerator Shake(float duration)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // After shaking, reset the position to the original position
        transform.position = originalPosition;
    }

    private void ResetBlock()
    {
        // Reset the block to its original position
        rb.isKinematic = true;
        rb.velocity = Vector2.zero; // Stop any ongoing movement
        transform.position = originalPosition;
        isFalling = false;
        specificCollider.enabled = true;
    }
}
