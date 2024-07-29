using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutableDestructible : MonoBehaviour, I_Transmutable
{
    public TransmutableInfo transmutableInfo;
    public float duration = 1.0f; // Duration of the scale decrease
    public Vector3 targetScale = new Vector3(0.1f, 0.1f, 0.1f); // Target scale
    public int currentType;

    // Start is called before the first frame update
    void Start()
    {
        currentType = transmutableInfo.startType;
    }

    

    public int GetDestructionNumber()
    {
        return transmutableInfo.destructionNumber;
    }

    public void InitiateAction()
    {
        StartCoroutine(DecreaseScale());
        //throw new System.NotImplementedException();
    }

    public bool IsDestroyable()
    {
        return transmutableInfo.isDestroyable;
    }

    IEnumerator DecreaseScale()
    {
        Vector3 initialScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the final scale is exactly the target scale
        transform.localScale = targetScale;
        Destroy(gameObject);
    }

    public int GetCurrentType()
    {
        return currentType;
    }

    public void SetCurrentType(int newType)
    {
        currentType = newType;
    }
}
