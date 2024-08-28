using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShakeTrigger : MonoBehaviour
{
    public CinemachineImpulseSource _impulseSource;
    private void Awake()
    {

    }

    public void TriggerShake(Vector2 dir)
    {
        _impulseSource.GenerateImpulse(dir.normalized);
    }
}
