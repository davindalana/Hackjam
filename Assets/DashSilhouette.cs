using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSilhouette : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Color color;

    public float fadeDuration = 0.5f; // Duration for the silhouette to fade out

    private void Awake()
    {
        color = spriteRenderer.color;
    }

    private void Update()
    {
        color.a -= Time.deltaTime / fadeDuration; // Reduce alpha over time
        spriteRenderer.color = color;

        if (color.a <= 0)
        {
            Destroy(gameObject); // Destroy the silhouette once fully transparent
        }
    }
}
