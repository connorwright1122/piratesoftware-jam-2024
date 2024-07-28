using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotGround : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FirstPersonController controller = other.gameObject.GetComponent<FirstPersonController>();
        if (controller != null)
        {
            //controller.SetToLastGroundedPosition();
            SceneSwapManager.ResetPlayerToLastGrounded();
        } else if (other.gameObject.CompareTag("Particle"))
        {
            Debug.Log("Le particle");
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("particle hit");
    }
}
