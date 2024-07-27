using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PlayerCombatController : MonoBehaviour
{

#if ENABLE_INPUT_SYSTEM
    private PlayerInput _playerInput;
#endif
    public GameObject _flaskObject;
    private Transform _flaskTransform;
    private Animator _flaskAnimator;
    private CharacterController _controller;
    private StarterAssetsInputs _input;
    private CameraFlashSpawner _cameraFlashSpawner;

    [SerializeField]
    public bool _canAttack = true;
    public float _timeSinceLastAttack;
    public float _primaryCooldown = 1f;

    private bool _isPrimaryCooldown;

    private bool _isZoom = false;
    [SerializeField]
    private int _flaskAttribute1 = 0;
    private int _flaskAttribute2 = 0;

    private bool _isInTab;
    //public CinemachineVirtualCamera _playerFollowCamera;
    public CinemachineVirtualCamera _playerUICamera;


    public AttackArea _attackArea;
    //public ParticleSystem _meleeParticle;
    //public GameObject _cameraFlashLight;

    public GameObject _inventoryGameobject;
    //private Canvas _inventoryCanvas;
    //private GraphicsRaycaster


    void Start()
    {
        _input = StarterAssetsInputs.Instance;
#if ENABLE_INPUT_SYSTEM
        _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif
        _attackArea = GetComponentInChildren<AttackArea>();
        //_meleeParticle = GetComponentInChildren<ParticleSystem>();
        _cameraFlashSpawner = GetComponent<CameraFlashSpawner>();
        _flaskAnimator = _flaskObject.GetComponent<Animator>();
        _flaskTransform = _flaskObject.transform;
    }

    void Update()
    {
        TabCheck();
        PrimaryCheck();
        
    }

    private void PrimaryCheck()
    {
        if (CanAttack())
        {
            if (_input.primary)
            {
                if (!_isZoom)
                {
                    Debug.Log("Attacked1");
                    Primary();
                }
            }
            else if (_input.secondary)
            {
                if (!_isZoom)
                {
                    Debug.Log("Attacked2");
                    Secondary();
                }
            }
            /*
            else if (_input.secondary)
            {

            }
            */
            else if (_input.zoom)
            {
                Zoom();
            }
        }
        _input.primary = false;
        _input.secondary = false;
        _input.zoom = false;
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

        //CameraFlash();
        _timeSinceLastAttack = 0f;
        //_meleeParticle.Play();
        _flaskAnimator.SetTrigger("FlaskDrink");
        StartCoroutine(PrimaryCooldown());
    }

    private void Secondary()
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

        //CameraFlash();
        _timeSinceLastAttack = 0f;
        //_meleeParticle.Play();
        _flaskAnimator.SetTrigger("FlaskSpray");
        StartCoroutine(PrimaryCooldown());
    }

    private void Zoom()
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

        //CameraFlash();
        //_timeSinceLastAttack = 0f;
        //_meleeParticle.Play();
        if (!_isZoom)
        {
            _flaskAnimator.SetTrigger("FlaskLook");
        } else
        {
            _flaskAnimator.SetTrigger("FlaskIdle");
        }
        _isZoom = !_isZoom;
        //StartCoroutine(PrimaryCooldown());
        
    }

    private bool CanAttack()
    {
        if (!_isPrimaryCooldown && _canAttack)
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

    public void SetFlaskAttributes(int value1, int value2)
    {
        _flaskAttribute1 = value1;
        _flaskAttribute2 = value2;
    }

    public int getFlaskAttribute1()
    {
        return _flaskAttribute1;
    }

    public int getFlaskAttribute2()
    {
        return _flaskAttribute2;
    }


    public void HandleFlask()
    {
        switch (getFlaskAttribute1())
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }

        switch (getFlaskAttribute2())
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    private void TabCheck()
    {
        if (_input.inventory) 
        {
            if (_isInTab) //exit inventory
            {
                _flaskAnimator.speed = 1;
                Cursor.lockState = CursorLockMode.Locked;
                _input.cursorInputForLook = true;
                _input.canMove = true;
                _playerUICamera.Priority = 9;
                _canAttack = true;
                _inventoryGameobject.GetComponent<Canvas>().enabled = false;
                _inventoryGameobject.GetComponent<GraphicRaycaster>().enabled = false;
                _isInTab = false;
                //_inventoryGameobject
            } 
            else //enter inventory
            {
                _flaskAnimator.speed = 0;
                Cursor.lockState = CursorLockMode.Confined;
                _input.cursorInputForLook = false;
                _input.canMove = false;
                _playerUICamera.Priority = 11;
                _canAttack = false;
                _inventoryGameobject.GetComponent<Canvas>().enabled = true;
                _inventoryGameobject.GetComponent<GraphicRaycaster>().enabled = true;
                _isInTab = true;
            }
        }
        _input.inventory = false;
    }



}
