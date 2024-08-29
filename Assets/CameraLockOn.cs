using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraLockOn : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    public Transform playerTransform; // Reference to the player's transform
    public float defaultOrthographicSize = 6f; // Default orthographic size

    // Define custom orthographic sizes based on camera lock object names
    public string[] cameraLockNames = { "cam7" };
    public float[] customOrthographicSizes = {8f};

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (_virtualCamera == null)
        {
            Debug.LogError("CinemachineVirtualCamera component is missing from the GameObject.");
        }
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
                Debug.Log($"Checking if {closestLock.name} matches {cameraLockNames[i]}"); // Debug log

                if (closestLock.name.Equals(cameraLockNames[i]))
                {
                    _virtualCamera.m_Lens.OrthographicSize = customOrthographicSizes[i];
                    Debug.Log($"Set camera size to {customOrthographicSizes[i]} for {closestLock.name}");
                    customSizeApplied = true;
                    break; // Exit after finding the matching name and applying the size
                }
            }

            if (!customSizeApplied)
            {
                _virtualCamera.m_Lens.OrthographicSize = defaultOrthographicSize;
                Debug.Log($"Set camera size to default {defaultOrthographicSize} for {closestLock.name}");
            }
        }
        else
        {
            Debug.LogWarning("No closest camera lock found.");
        }
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
