using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerBlocker : MonoBehaviour
{

    private FlaskManager flaskManager;
    
    private void OnTriggerEnter(Collider other)
    {
        //flaskManager = FindObjectOfType<FlaskManager>();
        flaskManager = other.gameObject.GetComponent<FlaskManager>();
        Debug.Log(flaskManager);
        flaskManager = other.gameObject.GetComponentInChildren<FlaskManager>();
        Debug.Log(flaskManager);
        if (flaskManager != null )
        {
            Debug.Log(flaskManager._foundGold + "g");
            Debug.Log(flaskManager._foundSilver + "s");
            Debug.Log(flaskManager._foundVitae + "v");
            if (flaskManager._isHidden && flaskManager._foundGold && flaskManager._foundSilver && flaskManager._foundVitae)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
