using UnityEngine;

[CreateAssetMenu(fileName = "NewNoteItem", menuName = "Inventory/NoteItem")]
public class NoteItem : ScriptableObject
{
    public string fileName;
    public string fileText; 
}
