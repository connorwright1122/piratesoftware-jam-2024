using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Interactable 
{
    //void 
    bool _canInteract { get; set; }
    void Interact();
}
