using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, I_Interactable
{
    public bool _canInteract { get; set; }

    public void Interact()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Interacted with");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
