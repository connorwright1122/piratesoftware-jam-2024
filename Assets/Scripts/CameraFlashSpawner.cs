using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CameraFlashSpawner : MonoBehaviour
{
    public ObjectPool<GameObject> _pool;
    public GameObject _flashPrefab;
    public Transform _flashSpawnPos;
    public Transform _cameraTransform;
    private GameObject _flashObj;
    
    private void Start()
    {
        
    }

    public void CreateFlash()
    {
        if (_flashObj == null)
        {
            _flashObj = Instantiate(_flashPrefab, _flashSpawnPos.position, _cameraTransform.rotation);
        } else
        {
            _flashObj.transform.position = _flashSpawnPos.position;
            _flashObj.transform.rotation = _cameraTransform.rotation;
        }
    }

    public void SetPool(ObjectPool<GameObject> pool)
    {

    }
}
