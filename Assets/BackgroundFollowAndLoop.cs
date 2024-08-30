using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollowAndLoop : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera's transform
    public float parallaxEffectMultiplier = 0.2f; // Coefficient for parallax effect
    public float loopDistance = 28.5f; // Distance after which the background loops

    private Vector3 lastCameraPosition;
    private float startPositionX;

    void Start()
    {
        // Store the initial camera position and background position
        lastCameraPosition = cameraTransform.position;
        transform.position = new Vector3(25f,0f,0f);
        startPositionX = transform.position.x;
    }

    void Update()
    {
        // Calculate how much the camera has moved since the last frame
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Apply parallax effect only on the X axis
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, deltaMovement.y * 0.05f, 0);

        // Store the camera position for the next frame
        lastCameraPosition = cameraTransform.position;

        // Calculate the relative distance moved from the start position
        float cameraRelativeX = cameraTransform.position.x - startPositionX;

        // Loop the background when it travels beyond the loop distance relative to the camera
        if (Mathf.Abs(cameraRelativeX) >= loopDistance)
        {
            float loopOffset = Mathf.Sign(cameraRelativeX) * loopDistance;
            startPositionX += loopOffset; // Update the start position
            transform.position = new Vector3(cameraTransform.position.x - (cameraRelativeX - loopOffset), transform.position.y, transform.position.z);
        }
    }
}
