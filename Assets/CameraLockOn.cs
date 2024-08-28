using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraLockOn : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    public Transform playerTransform; // Reference to the player's transform

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        GameObject closestLock = FindClosestCameraLock();

        if (closestLock != null)
        {
            // Set the camera to look at the closest cameralock
            //_virtualCamera.LookAt = closestLock.transform;
            _virtualCamera.Follow = closestLock.transform;
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
