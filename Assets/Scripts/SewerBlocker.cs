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
            if (flaskManager._isHidden && flaskManager._foundGold && flaskManager._foundSilver && flaskManager._foundVitae)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
