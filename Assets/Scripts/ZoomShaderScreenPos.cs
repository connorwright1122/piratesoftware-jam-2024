using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomShaderScreenPos : MonoBehaviour
{
    [SerializeField] private Material _material;

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPixels = Camera.main.ScreenToWorldPoint(transform.position);
        screenPixels = new Vector2(screenPixels.x / Screen.width, screenPixels.y / Screen.height);
        _material.SetVector("ObjectScreenPosition", screenPixels);
    }
}
