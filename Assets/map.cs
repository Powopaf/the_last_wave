using System;
using UnityEngine;

public class Game : MonoBehaviour
{ 
    private void Start()
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0, -2);
    }

    void Update()
    {
            
    }
}