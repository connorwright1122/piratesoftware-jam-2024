using StarterAssets;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Windows;

public class Detector : MonoBehaviour
{
    public float maxDistance = 5f;
    //public LayerMask itemLayer;
    //public Inventory inventory; // Reference to the player's inventory
    public Slider detectionSlider;
    private bool objectDetected = false;

    public GameObject noteButtonPrefab;
    public Transform scrollViewContent;
    public TextMeshProUGUI noteTextBox;

    private StarterAssetsInputs _input;

    public FlaskManager flaskManager;


    private void Start()
    {
        //detectionSlider = GetComponentInChildren<Slider>();
        _input = StarterAssetsInputs.Instance;
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

        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interact button");
            _input.interact = true;
        } else
        {
            _input.interact = false;
        }
        */
    }

    void DetectItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            //Debug.Log(hit.transform.gameObject.name);
            I_Interactable interactable = hit.collider.GetComponent<I_Interactable>();
            //Debug.Log(interactable);
            if (interactable != null)
            {
                objectDetected = true;
                //Debug.Log(_input.interact);
                if (objectDetected && Input.GetKeyDown(KeyCode.E))
                //if (objectDetected && _input.interact)
                {
                    //Debug.Log(_input.interact);
                    interactable.Interact();
                }
                _input.interact = false;
            } else
            {
                objectDetected = false;
            }

            NoteItemHolder noteHolder = hit.collider.GetComponent<NoteItemHolder>();
            if (noteHolder != null && !noteHolder.HasBeenCollected)
            {
                AddNoteToScrollView(noteHolder.noteItem);
                objectDetected = true;
                noteHolder.HasBeenCollected = true;  // Prevent multiple collections
            }

            AlchemyItemHolder alcItem = hit.collider.GetComponent<AlchemyItemHolder>();
            if (alcItem != null && !alcItem.HasBeenCollected)
            {
                //AddNoteToScrollView(noteHolder.noteItem);
                flaskManager.UnlockElement(alcItem.alchemyNum);
                objectDetected = true;
                alcItem.HasBeenCollected = true;  // Prevent multiple collections
            }

            /*
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
            */

            //Debug.Log(hit.transform.gameObject.name);
            //Debug.Log(hit.gameObject.ToString());
        }
        else
        {
            objectDetected = false;
        }

        
    }

    void UpdateSlider()
    {
        float targetValue = objectDetected ? 1f : 0f;
        detectionSlider.value = Mathf.Lerp(detectionSlider.value, targetValue, Time.deltaTime * 2f);
    }

    void AddNoteToScrollView(NoteItem noteItem)
    {
        // Instantiate the note button prefab
        GameObject newButton = Instantiate(noteButtonPrefab, scrollViewContent);

        //Increase the height of the content
        RectTransform rt = scrollViewContent.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 48);


        // Set the title of the note button
        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = noteItem.fileName;

        // Add an onClick listener to the button
        Button buttonComponent = newButton.GetComponent<Button>();
        buttonComponent.onClick.AddListener(() => DisplayNoteContent(noteItem));
        buttonComponent.onClick.AddListener(() => SoundFXManager.Instance.PlaySoundFXClipUI());
    }

    public void DisplayNoteContent(NoteItem noteItem)
    {
        // Set the text of the noteTextBox to the note content
        //noteTextBox.
        if (noteItem.centered)
        {
            noteTextBox.alignment = TextAlignmentOptions.TopGeoAligned;
        } else
        {
            noteTextBox.alignment = TextAlignmentOptions.TopLeft;
        }
        noteTextBox.text = noteItem.fileText;
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
    }

}
