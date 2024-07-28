using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventManager : MonoBehaviour
{
    public Transform _particleSpawnLocation;
    public Transform _particleRotation;
    public ParticleSystem _particleSystem;
    public FlaskManager _flaskManager;

    public void PlayParticleSystem()
    {
        _particleSystem.transform.position = _particleSpawnLocation.position;
        //_particleSystem.transform.localRotation = Quaternion.Euler(_particleRotation.transform.forward);
        //_particleSystem.transform.rotation = Quaternion.Euler(_particleRotation.transform.TransformDirection(forward));
        //_particleSystem.transform.rotation = _particleRotation.transform.localRotation; // straight up
        _particleSystem.transform.rotation = _particleRotation.transform.rotation;
        _particleSystem.Play();
    }

    public void NotifyDrinkEvent()
    {
        _flaskManager.FlaskDrink();
    }
}
