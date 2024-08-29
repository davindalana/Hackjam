using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraLockOn : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    public Transform playerTransform; // Reference to the player's transform
    public float defaultOrthographicSize = 6f; // Default orthographic size
    public float smoothTime = 0.5f; // The time it takes to reach the target size

    // Define custom orthographic sizes based on camera lock object names
    public string[] cameraLockNames = { "cam7" };
    public float[] customOrthographicSizes = { 8f };

    private float targetOrthographicSize; // The size to transition to
    private float currentVelocity = 0.0f; // Used for smooth damping

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (_virtualCamera == null)
        {
            Debug.LogError("CinemachineVirtualCamera component is missing from the GameObject.");
        }

        // Initialize the target size with the default size
        targetOrthographicSize = defaultOrthographicSize;
    }

    private void Update()
    {
        GameObject closestLock = FindClosestCameraLock();

        if (closestLock != null)
        {
            _virtualCamera.Follow = closestLock.transform;

            bool customSizeApplied = false;

            // Check if there's a custom orthographic size for this camera lock based on its name
            for (int i = 0; i < cameraLockNames.Length; i++)
            {
                if (closestLock.name.Equals(cameraLockNames[i]))
                {
                    targetOrthographicSize = customOrthographicSizes[i];
                    customSizeApplied = true;
                    break; // Exit after finding the matching name and applying the size
                }
            }

            if (!customSizeApplied)
            {
                targetOrthographicSize = defaultOrthographicSize;
            }
        }
        else
        {
            Debug.LogWarning("No closest camera lock found.");
            targetOrthographicSize = defaultOrthographicSize;
        }

        // Smoothly transition to the target orthographic size
        _virtualCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(
            _virtualCamera.m_Lens.OrthographicSize,
            targetOrthographicSize,
            ref currentVelocity,
            smoothTime
        );
    }

    private GameObject FindClosestCameraLock()
    {
        GameObject[] cameraLocks = GameObject.FindGameObjectsWithTag("cameraLock");
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 playerPosition = playerTransform.position;

        foreach (GameObject lockOn in cameraLocks)
        {
            float distance = Vector3.Distance(lockOn.transform.position, playerPosition);
            if (distance < minDistance)
            {
                closest = lockOn;
                minDistance = distance;
            }
        }

        return closest;
    }
}
