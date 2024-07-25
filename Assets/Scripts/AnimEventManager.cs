using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventManager : MonoBehaviour
{
    public Transform _particleSpawnLocation;
    public ParticleSystem _particleSystem;

    public void PlayParticleSystem()
    {
        _particleSystem.transform.position = _particleSpawnLocation.position;
        _particleSystem.Play();
    }
}
