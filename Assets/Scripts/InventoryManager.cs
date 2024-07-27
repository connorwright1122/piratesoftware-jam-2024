using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public TextMeshPro notesText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNotesText(string newText)
    {
        string newText1 = newText.Replace("{\n}", "\n");
        notesText.SetText(newText1);
        notesText.text.Replace("{\n}", "\n");
    }
}
