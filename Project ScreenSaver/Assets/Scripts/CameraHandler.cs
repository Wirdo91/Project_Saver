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
        _camera.orthographicSize = 540 * Settings.scale;
        transform.position = new Vector3((Screen.width * Settings.scale) / 2f, (Screen.height * Settings.scale) / 2f, -10);
    }
}
