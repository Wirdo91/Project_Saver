using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class Floor : MonoBehaviour
{
    public static Floor instance;
    
    private Mesh _floorMesh;
    private Texture2D _floorTexture;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _floorMesh = new Mesh();

        int width = (int)(Screen.width * Settings.scale);
        int height = (int)(Screen.height * Settings.scale);

        _floorMesh.vertices = new[]
        {
            Vector3.zero, 
            new Vector3(width, 0),
            new Vector3(width, height),
            new Vector3(0, height)
        };
        _floorMesh.triangles = new[]
        {
            0,2,1,
            0,3,2
        };
        _floorMesh.normals = new[]
        {
            Vector3.back, 
            Vector3.back, 
            Vector3.back, 
            Vector3.back
        };
        _floorMesh.uv = new[]
        {
            Vector2.zero, 
            Vector2.right, 
            Vector2.one, 
            Vector2.up
        };
        
        GetComponent<MeshFilter>().mesh = _floorMesh;
        
        _floorTexture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _floorTexture.SetPixel(x,y,Color.white);
            }
        }
        _floorTexture.Apply();
        
        GetComponent<MeshRenderer>().material.mainTexture = _floorTexture;
    }

    void LateUpdate()
    {
        if (_bloodToBeAdded.Count > 0)
        {
            foreach (var i in _bloodToBeAdded)
            {
                _floorTexture.SetPixel(i.x, i.y, Color.red);
            }
            
            _floorTexture.Apply();
            _bloodToBeAdded.Clear();
        }
    }

    private List<Vector2Int> _bloodToBeAdded = new List<Vector2Int>();
    public void AddBloodSplat(Vector2Int position)
    {
        _bloodToBeAdded.Add(position);
    }
}
