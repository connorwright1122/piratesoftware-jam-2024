using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour, I_Interactable
{
    public enum DoorToSpawnAt
    {
        None,
        One,
        Two,
        Three,
        Four
    }
    
    public bool _canInteract { get; set; }
    [Header("Spawn To")]
    [SerializeField] private DoorToSpawnAt _doorToSpawnTo;
    [SerializeField] private SceneField _sceneToLoad;

    [Space(10f)]
    [Header("This Door")]
    public DoorToSpawnAt _currentDoorPosition;

    public void Interact()
    {
        //throw new System.NotImplementedException();
        SceneSwapManager.SwapSceneFromDoorUse(_sceneToLoad, _doorToSpawnTo);
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
