using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePivot : MonoBehaviour, I_Interactable
{

    //private bool _canInteract { get; set; }
    bool I_Interactable._canInteract { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private bool canInteract;
    public float degrees;
    public float duration;

    private void Start()
    {
        canInteract = true;
    }

    public void Interact()
    {
        if (canInteract) {
            canInteract = false;
            StartCoroutine(RotateByDegrees(degrees, duration));
        }
    }

    IEnumerator RotateByDegrees(float degrees, float duration)
    {
        // Record the starting rotation
        Quaternion startRotation = transform.rotation;

        // Calculate the end rotation
        Quaternion endRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, degrees, 0));

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the fraction of time completed
            float t = elapsedTime / duration;

            // Lerp between the start and end rotations
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            // Update elapsed time
            elapsedTime += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Ensure the final rotation is exactly the end rotation
        transform.rotation = endRotation;
    }
}


