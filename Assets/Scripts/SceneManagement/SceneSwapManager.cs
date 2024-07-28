using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapManager : MonoBehaviour
{
    public static SceneSwapManager Instance;

    private static bool _loadFromDoor;

    private GameObject _player;
    private Collider _playerCollider;
    private Collider _doorCollider;
    private Vector3 _playerSpawnPosition;


    private InteractableDoor.DoorToSpawnAt _doorToSpawnTo;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerCollider = _player.GetComponent<Collider>();

        SceneFadeManager.Instance.InstantFadeOut();
        SceneFadeManager.Instance.StartFadeIn();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static void SwapSceneFromDoorUse(SceneField myScene, InteractableDoor.DoorToSpawnAt doorToSpawnAt)
    {
        _loadFromDoor = true;
        Instance.StartCoroutine(Instance.FadeOutThenChangeScene(myScene, doorToSpawnAt));
    }

    private IEnumerator FadeOutThenChangeScene(SceneField myScene, InteractableDoor.DoorToSpawnAt doorToSpawnAt = InteractableDoor.DoorToSpawnAt.None)
    {
        SceneFadeManager.Instance.StartFadeOut();

        while (SceneFadeManager.Instance.IsFadingOut)
        {
            yield return null;
        }

        _doorToSpawnTo = doorToSpawnAt;
        SceneManager.LoadScene(myScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneFadeManager.Instance.StartFadeIn();

        if (_loadFromDoor)
        {
            FindDoor(_doorToSpawnTo);
            _player.transform.position = _playerSpawnPosition;
            _loadFromDoor = false;
        }
    }

    private void FindDoor(InteractableDoor.DoorToSpawnAt doorSpawnNumber)
    {
        InteractableDoor[] doors = FindObjectsOfType<InteractableDoor>();

        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i]._currentDoorPosition == doorSpawnNumber)
            {
                _doorCollider = doors[i].gameObject.GetComponent<Collider>();
                CalculateSpawnPosition();
                return;
            }
        }
    }

    private void CalculateSpawnPosition()
    {
        float colliderHeight = _playerCollider.bounds.extents.y;
        _playerSpawnPosition = _doorCollider.transform.position - new Vector3(0f, colliderHeight, 0f);
    }
}
