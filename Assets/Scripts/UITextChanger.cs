using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextChanger : MonoBehaviour
{
    private NoteItemHolder holder;
    private NoteItem note;
    public TextMeshProUGUI noteViewer;

    private void Start()
    {
        holder = GetComponent<NoteItemHolder>();
        note = holder.noteItem;
    }

    public void SetNoteViewerText()
    {
        noteViewer.text = note.fileText;
    }
}
