using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraHandler : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void Start()
    {
        _camera.orthographicSize = 540 / Settings.zoom;
        transform.position = new Vector3((Screen.width / Settings.zoom) / 2f, (Screen.height / Settings.zoom) / 2f, -10);
    }
}
