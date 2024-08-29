using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public SpriteMask circleMask; // Reference to the Sprite Mask
    public GameObject deathParticlePrefab; // Reference to the death particle prefab
    public Transform playerTransform; // Reference to the player transform
    public Camera mainCamera; // Reference to the main camera
    [SerializeField] private float zoomDuration = 1.0f; // Duration of the zoom effect
    [SerializeField] private Vector3 zoomScale = new Vector3(0.1f, 0.1f, 1f); // Final scale for zoom in (small circle)
    [SerializeField] private Vector3 originalScale = new Vector3(400f, 400f, 1f); // Starting scale of the Sprite Mask
    public GameObject blackOverlay;
    public Animator playerAnimator; // Reference to the Animator component
    public CameraShakeTrigger cameraShakeTrigger; // Reference to the CameraShakeTrigger component

    private PlayerMovement playerMovement; // Reference to the player movement script
    private bool isDead = false;
    [SerializeField]
    private Transform lastCheckpoint; // Reference to the last touched checkpoint

    [SerializeField]
    private static Vector3 lastCheckpointPosition;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        // Assuming originalScale is already set to the initial scale of the Sprite Mask
        circleMask.transform.localScale = originalScale;
        blackOverlay.SetActive(false);

        GameManager.LoadGame(playerTransform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            // Find the child transform named "SpawnPoint"
            Transform spawnPoint = other.transform.Find("Spawnpoint");

            if (spawnPoint != null)
            {
                lastCheckpoint = spawnPoint;
                lastCheckpointPosition = lastCheckpoint.position; // Store the position for debugging
            }
            else
            {
                Debug.LogWarning("SpawnPoint not found as a child of the checkpoint.");
                lastCheckpoint = other.transform;
                lastCheckpointPosition = lastCheckpoint.position;
            }
            GameManager.SaveGame(lastCheckpoint.position);
        }
    }


    public void HandlePlayerDeath()
    {
        if (isDead) return; // Prevent multiple triggers

        isDead = true;
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        // 1. Stop player movement and input
        playerMovement.enabled = false;
        Rigidbody2D playerRigidbody = playerTransform.GetComponent<Rigidbody2D>();
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        // 2. Trigger the death animation
        playerAnimator.SetBool("Dead", true);
        yield return new WaitForSeconds(0.001f);
        playerAnimator.SetBool("Dead", false);
        // 3. Trigger screen shake
        cameraShakeTrigger.TriggerShake(new Vector2(1,1)); // Trigger shake with default settings
        GameObject deathParticles = Instantiate(deathParticlePrefab, playerTransform.position + new Vector3(0,-0.5f,0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        // 4. Spawn death particle effect

        // 5. Position the circular mask at the player's position
        PositionCircularMask();

        // 6. Black screen zoom in effect
        yield return StartCoroutine(CircularZoomIn());

        // 7. Respawn the player at the last checkpoint
        RespawnPlayer();

        // 8. Black screen zoom out effect
        yield return StartCoroutine(CircularZoomOut());

        // 9. Re-enable player movement and input
        playerMovement.enabled = true;

        // Destroy the death particles after they have finished playing
        Destroy(deathParticles, 2.0f); // Adjust the time as needed based on the particle system duration
        isDead = false;
        playerRigidbody.constraints = RigidbodyConstraints2D.None;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void PositionCircularMask()
    {
        // Position the circular mask and black overlay at the player's position
        circleMask.transform.position = new Vector2(playerTransform.position.x, playerTransform.position.y);
        blackOverlay.transform.position = new Vector2(playerTransform.position.x, playerTransform.position.y);
        blackOverlay.SetActive(true);
    }

    private IEnumerator CircularZoomIn()
    {
        circleMask.gameObject.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / zoomDuration;
            circleMask.transform.localScale = Vector3.Lerp(originalScale, zoomScale, t);
            yield return null;
        }
        circleMask.transform.localScale = zoomScale;
    }

    private IEnumerator CircularZoomOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / zoomDuration;
            circleMask.transform.localScale = Vector3.Lerp(zoomScale, originalScale, t);
            yield return null;
        }
        circleMask.transform.localScale = originalScale;
        circleMask.gameObject.SetActive(false);
        blackOverlay.SetActive(false);
    }

    private void RespawnPlayer()
    {
        if (lastCheckpoint != null)
        {
            transform.position = lastCheckpoint.position;
        }
        else
        {
            Debug.LogWarning("No checkpoint has been touched yet!");
        }
        playerAnimator.SetBool("Dead", false); // Reset the Dead trigger to return to normal animations
    }


}
