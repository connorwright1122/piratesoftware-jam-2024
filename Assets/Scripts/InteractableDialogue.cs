using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractableDialogue : MonoBehaviour, I_Interactable
{
    public bool _canInteract { get; set; }
    [SerializeField] private string _startNode;
    private DialogueRunner _dialogueRunner;

    private void Start()
    {
        _dialogueRunner = FindObjectOfType<DialogueRunner>();
        Debug.Log(_dialogueRunner);
    }

    public void Interact()
    {
        
        _dialogueRunner.StartDialogue(_startNode);
        Debug.Log("Interacted");
        //throw new System.NotImplementedException();
    }


    
}
