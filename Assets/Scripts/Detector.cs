using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetector : MonoBehaviour
{
    public float maxDistance = 5f;
    //public LayerMask itemLayer;
    //public Inventory inventory; // Reference to the player's inventory
    public Slider detectionSlider;
    private bool objectDetected = false;

    private void Start()
    {
        //detectionSlider = GetComponentInChildren<Slider>();
    }

    void Update()
    {
        /*
        if (Input.GetButtonDown("Fire1")) // You can change this to whatever input you want
        {
            
        }
        */

        DetectItem();
        UpdateSlider();

    }

    void DetectItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            
            I_Interactable itemObject = hit.collider.GetComponent<I_Interactable>();
            if (itemObject != null)
            {
                //inventory.AddItem(itemObject.item);
                objectDetected = true;
                Debug.Log(hit.transform.gameObject.name);
            }
            else
            {
                objectDetected = false;
            }
            
            //Debug.Log(hit.transform.gameObject.name);
            //Debug.Log(hit.gameObject.ToString());
        } else
        {
            objectDetected = false;
        }
    }

    void UpdateSlider()
    {
        float targetValue = objectDetected ? 1f : 0f;
        detectionSlider.value = Mathf.Lerp(detectionSlider.value, targetValue, Time.deltaTime * 2f);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
    }

}
