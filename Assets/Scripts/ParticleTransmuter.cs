using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTransmuter : MonoBehaviour
{
    public FlaskManager _flaskManager;

    /*
     * case 0: // nothing
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
     * */

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("particle collided");
        I_Transmutable transmutable = other.GetComponent<I_Transmutable>();
        if (transmutable != null)
        {
            int att1 = _flaskManager.getFlaskAttribute1();
            int att2 = _flaskManager.getFlaskAttribute2();

            if (transmutable.IsDestroyable())
            {
                if (att1 == transmutable.GetDestructionNumber() || att2 == transmutable.GetDestructionNumber())
                {
                    transmutable.InitiateDestruction();
                }
            } else
            {

            }

            /*
            if (att1 == 1) //light
            { 
                //area light on
            } else if () //fire
            {
                //play fire particle system
                //enable area light
            } else if () //water
            {
                //clean object 
                //Destroy(other.gameObject);
                
            }
            */

        }
    }
}
