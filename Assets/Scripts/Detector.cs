using UnityEditor.Rendering;
using UnityEngine;

public class ItemDetector : MonoBehaviour
{
    public float maxDistance = 5f;
    //public LayerMask itemLayer;
    //public Inventory inventory; // Reference to the player's inventory

    void Update()
    {
        /*
        if (Input.GetButtonDown("Fire1")) // You can change this to whatever input you want
        {
            
        }
        */

        DetectItem();
    }

    void DetectItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            /*
            ItemObject itemObject = hit.collider.GetComponent<ItemObject>();
            if (itemObject != null)
            {
                inventory.AddItem(itemObject.item);
            }
            */
            Debug.Log(hit.transform.gameObject.name);
            //Debug.Log(hit.gameObject.ToString());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
    }

}
