using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public float hoverSpeed = 1f; // Speed of the hover animation
    public float hoverHeight = 0.5f; // Height of the hover animation
    public GameObject letterContent; // The object that represents the letter's content
    public bool isInteracting = false; // Track whether the letter is opened or closed
    public float fadeDuration = 0.5f; // Duration of the fade in/out
    public Vector3 contentOffset = new Vector3(0, 0, 5f); // Offset from the camera when the letter is opened

    private Vector3 initialPosition;
    private bool isHovering = true;
    private bool isPlayerInside = false; // Track whether the player is within the collider
    private Renderer[] renderers; // To control the fade effect

    private void Start()
    {
        initialPosition = transform.position;

        if (letterContent != null)
        {
            // Get all renderers in the letter content to control their visibility
            renderers = letterContent.GetComponentsInChildren<Renderer>();
            SetRenderersAlpha(0f); // Start with the content hidden
            letterContent.SetActive(false); // Initially hide the letter content
        }
    }

    private void Update()
    {
        HandleHoverAnimation();

        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (!isInteracting)
            {
                OpenLetter();
            }
            else
            {
                CloseLetter();
            }
        }
    }

    private void HandleHoverAnimation()
    {
        if (isHovering)
        {
            // Simple hover animation using sine wave
            float newY = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight + initialPosition.y;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    private void OpenLetter()
    {
        isInteracting = true;
        isHovering = false; // Stop hovering when interacting

        if (letterContent != null)
        {
            letterContent.SetActive(true); // Make sure the content is active
            //PositionContentInFrontOfCamera();
            StartCoroutine(FadeRenderersIn()); // Fade in
        }

        Debug.Log("Letter opened");
    }

    private void CloseLetter()
    {
        isInteracting = false;
        isHovering = true; // Resume hovering when not interacting

        if (letterContent != null)
        {
            StartCoroutine(FadeRenderersOut()); // Fade out
        }

        Debug.Log("Letter closed");
    }

    private void PositionContentInFrontOfCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            letterContent.transform.position = mainCamera.transform.position + mainCamera.transform.forward * contentOffset.z;
            letterContent.transform.rotation = mainCamera.transform.rotation;
        }
    }

    private IEnumerator FadeRenderersIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            SetRenderersAlpha(alpha);
            yield return null;
        }
        SetRenderersAlpha(1f);
    }

    private IEnumerator FadeRenderersOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            SetRenderersAlpha(alpha);
            yield return null;
        }
        SetRenderersAlpha(0f);
        letterContent.SetActive(false);
    }

    private void SetRenderersAlpha(float alpha)
    {
        foreach (Renderer renderer in renderers)
        {
            Color color = renderer.material.color;
            color.a = alpha;
            renderer.material.color = color;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            Debug.Log("Player entered letter's collider");
            OpenLetter();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            Debug.Log("Player exited letter's collider");
            CloseLetter();
        }
    }
}
