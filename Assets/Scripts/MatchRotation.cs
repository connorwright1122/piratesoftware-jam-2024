/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchRotation : MonoBehaviour
{
    public GameObject changeThisTransform;
    public Transform rotationToMatch;
    public Transform positionToMatch;
    //private Vector3 rotat
    private Animator animator;
    private bool cr_running;
    public float lerpDuration = 1.2f;


    // Start is called before the first frame update
    void Start()
    {
        animator = changeThisTransform.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("FlaskIdle"))
        {
            StartCoroutine(WaitForAnimationFinished());
        } else
        {
            if (!cr_running)
            {
                StopCoroutine(WaitForAnimationFinished());
            }
        }
    }

    public IEnumerator WaitForAnimationFinished()
    {
        cr_running = true;
        yield return new WaitUntil(delegate { return animator.GetCurrentAnimatorStateInfo(0).IsName("FlaskIdle"); });

        // Move the UI-Elements from the animation out of the canvas
        //changeThisTransform.transform.rotation = rotationToMatch.rotation;
        //changeThisTransform.transform.position = rotationToMatch.position;

        yield return StartCoroutine(LerpTransform(changeThisTransform.transform, rotationToMatch, lerpDuration));



        cr_running = false;
        Debug.Log("Transforming?");
    }

    private IEnumerator LerpTransform(Transform fromTransform, Transform toTransform, float duration)
    {
        float time = 0;
        Quaternion startRotation = fromTransform.rotation;
        Vector3 startPosition = fromTransform.position;
        Quaternion endRotation = toTransform.rotation;
        Vector3 endPosition = toTransform.position;

        while (time < duration)
        {
            time += Time.deltaTime;
            fromTransform.rotation = Quaternion.Lerp(startRotation, endRotation, time / duration);
            fromTransform.position = Vector3.Lerp(startPosition, endPosition, time / duration);
            yield return null;
        }

        fromTransform.rotation = endRotation;
        fromTransform.position = endPosition;

        Debug.Log("Rotating?");
    }

}
*/
using System.Collections;
using UnityEngine;

public class MatchRotation : MonoBehaviour
{
    public GameObject changeThisTransform;
    public Transform rotationToMatch;
    public Transform positionToMatch;
    private Animator animator;
    private bool cr_running;
    public float lerpDuration = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        animator = changeThisTransform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!cr_running && !animator.GetCurrentAnimatorStateInfo(0).IsName("FlaskIdle"))
        {
            StartCoroutine(WaitForAnimationFinished());
        }
    }

    public IEnumerator WaitForAnimationFinished()
    {
        cr_running = true;
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("FlaskIdle"));

        // Move the UI-Elements from the animation out of the canvas
        yield return StartCoroutine(LerpTransform(changeThisTransform.transform, rotationToMatch, positionToMatch, lerpDuration));

        cr_running = false;
        Debug.Log("Transforming?");
    }

    private IEnumerator LerpTransform(Transform fromTransform, Transform toRotation, Transform toPosition, float duration)
    {
        float elapsedTime = 0;

        Quaternion startRotation = fromTransform.rotation;
        Vector3 startPosition = fromTransform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float lerpFactor = elapsedTime / duration;

            // Smoothly interpolate towards the target rotation and position
            fromTransform.rotation = Quaternion.Lerp(startRotation, toRotation.rotation, lerpFactor);
            fromTransform.position = Vector3.Lerp(startPosition, toPosition.position, lerpFactor);

            yield return null;
        }

        // Ensure the final rotation and position match exactly
        fromTransform.rotation = toRotation.rotation;
        fromTransform.position = toPosition.position;

        Debug.Log("Rotating?");
    }
}
