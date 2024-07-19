using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerCombatController : MonoBehaviour
{

#if ENABLE_INPUT_SYSTEM
    private PlayerInput _playerInput;
#endif
    private Animator _animator;
    private CharacterController _controller;
    private StarterAssetsInputs _input;
    private CameraFlashSpawner _cameraFlashSpawner;

    [SerializeField]
    public bool _canAttack = true;
    public float _timeSinceLastAttack;
    public float _primaryCooldown = 1f;

    private bool _isPrimaryCooldown;


    //public AttackArea _attackArea;
    //public ParticleSystem _meleeParticle;
    //public GameObject _cameraFlashLight;

    void Start()
    {
        _input = StarterAssetsInputs.Instance;
#if ENABLE_INPUT_SYSTEM
        _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif
        //_attackArea = GetComponentInChildren<AttackArea>();
        //_meleeParticle = GetComponentInChildren<ParticleSystem>();
        _cameraFlashSpawner = GetComponent<CameraFlashSpawner>();
    }

    void Update()
    {
        PrimaryCheck();
    }

    private void PrimaryCheck()
    {
        if (CanAttack())
        {
            if (_input.primary)
            {
                Debug.Log("Attacked1");
                Primary();
            }
        }
        _input.primary = false;
    }

    private void Primary()
    {
        /*
        foreach (var damageable in _attackArea.GetDamageablesInRange())
        {
            damageable.TakeDamage(10);
            if (damageable.IsDestroyed())
            {
                IncreaseSizeAndPower();
            }
            Debug.Log("Attacked");
        }
        */

        CameraFlash();
        _timeSinceLastAttack = 0f;
        //_meleeParticle.Play();
        StartCoroutine(PrimaryCooldown());
    }

    private bool CanAttack()
    {
        if (!_isPrimaryCooldown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private IEnumerator PrimaryCooldown()
    {
        _isPrimaryCooldown = true;
        yield return new WaitForSeconds(_primaryCooldown);
        _isPrimaryCooldown = false;
    }

    
    private void CameraFlash()
    {
        _cameraFlashSpawner.CreateFlash();
    }



}
