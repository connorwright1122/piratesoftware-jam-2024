using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
//using static UnityEngine.Rendering.DebugUI;
using TMPro;

public class FlaskManager : MonoBehaviour
{
    public GameObject _flask;
    public GameObject _flaskLiquid;
    public GameObject _invisQuad;
    public Light _flaskLight;
    public FirstPersonController _characterController;
    public int _flaskAttribute1 = 0;
    public int _flaskAttribute2 = 0;
    public ParticleSystem _splashParticles;

    private Material _flaskLiquidMaterial;
    public Material _flaskGlassMaterial;

    private Color _defaultGlassColor;
    private Color _hiddenGlassColor;

    private bool _isHidden;

    public TMP_Dropdown _flaskDropdown1;
    public TMP_Dropdown _flaskDropdown2;


    void Start()
    {
        _flaskLiquidMaterial = _flaskLiquid.GetComponent<Renderer>().material;
        _characterController = GetComponent<FirstPersonController>();
        _flaskLight = GetComponentInChildren<Light>();
        //_flaskGlassMaterial = _invisQuad.GetComponent<Material>();
        _defaultGlassColor = _flaskGlassMaterial.color;
        _hiddenGlassColor = new Color(_defaultGlassColor.r, _defaultGlassColor.g, _defaultGlassColor.b, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getFlaskAttribute1()
    {
        return _flaskAttribute1;
    }

    public int getFlaskAttribute2()
    {
        return _flaskAttribute2;
    }

    public void SetFlaskAttributes(int value1, int value2)
    {
        if (value1 != _flaskAttribute1 || value2 != _flaskAttribute2)
        {
            SetFlaskMaterial(value1, value2);
        } 
        
        _flaskAttribute1 = value1;
        _flaskAttribute2 = value2;
    }

    public void UpdateFlaskAttributes()
    {
        SetFlaskAttributes(_flaskDropdown1.value, _flaskDropdown2.value);
        Debug.Log("New Flask Values: " + _flaskDropdown1.value + " and " + _flaskDropdown2.value);
    }

    public void SetFlaskMaterial(int value1, int value2)
    {
        /*
        switch (value1)
        {
            case 1: // light
                _flaskLiquidMaterial.SetFloat("_Emission", 5);
                break;
            case 2: // dark
                _flaskLiquidMaterial.SetFloat("_Emission", -1);
                break;
            default:
                _flaskLiquidMaterial.SetFloat("_Emission", 0);
                break;
        }
        */

        if (value1 == 1 || value2 == 1)
        {
            _flaskLiquidMaterial.SetFloat("_Emission", 5);
            var lights = _splashParticles.lights;
            lights.enabled = true;
        }
        else if (value1 == 2 || value2 == 2)
        {
            _flaskLiquidMaterial.SetFloat("_Emission", -1);
            var lights = _splashParticles.lights;
            lights.enabled = false;
        }
        else
        {
            _flaskLiquidMaterial.SetFloat("_Emission", 0);
            var lights = _splashParticles.lights;
            lights.enabled = false;
        }

        /*
        switch (value2)
        {
            case 0: // nothing
                //_flaskLiquidMaterial.SetFloat("_Emission", 0);
                break;
            case 1: // fire - fire particles
                //_flaskLiquidMaterial.SetFloat("_Emission", 5);
                break;
            case 2: // water - transmute
                //_flaskLiquidMaterial.SetFloat("_Emission", -1);
                break;
        }
        */


    }


    public void FlaskDrink()
    {
        ResetStats();
        switch (getFlaskAttribute1())
        {
            case 0: // nothing
                break;
            case 1: // light
                _flaskLight.intensity += 2f;
                break;
            case 2: // dark
                _invisQuad.GetComponent<MeshRenderer>().enabled = true;
                _flaskGlassMaterial.color = _hiddenGlassColor;
                _isHidden = true;
                break;
            case 3: // fire
                _characterController.IncreaseSpeedMultiplier();
                break;
            case 4: // water
                _characterController.DecreaseGravityMultiplier();
                break;
            case 5: // air
                _characterController.IncreaseJumpMultiplier();
                break;

        }

        switch (getFlaskAttribute2())
        {
            case 0: // nothing
                break;
            case 1: // light
                _flaskLight.intensity += 2f;
                break;
            case 2: // dark
                _invisQuad.GetComponent<MeshRenderer>().enabled = true;
                _flaskGlassMaterial.color = _hiddenGlassColor;
                _isHidden = true;
                break;
            case 3: // fire
                _characterController.IncreaseSpeedMultiplier();
                break;
            case 4: // water
                _characterController.DecreaseGravityMultiplier();
                break;
            case 5: // air
                _characterController.IncreaseJumpMultiplier();
                break;
        }
    }

    public void ResetStats()
    {
        _flaskLight.intensity = 0f;
        _characterController.ResetToDefaultMultipliers();
        _flaskGlassMaterial.color = _defaultGlassColor;
        _isHidden = false;
        _invisQuad.GetComponent<MeshRenderer>().enabled = false;
    }

    /*
    public void FlaskDrink()
    {
        switch (getFlaskAttribute1())
        {
            case 0: // nothing
                break;
            case 1: // light
                break;
            case 2: // dark
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
    */



    /*
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
     * */
}
