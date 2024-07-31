using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerBlocker : MonoBehaviour
{

    private FlaskManager flaskManager;
    private void OnCollisionEnter(Collision collision)
    {
        flaskManager = FindObjectOfType<FlaskManager>();
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
