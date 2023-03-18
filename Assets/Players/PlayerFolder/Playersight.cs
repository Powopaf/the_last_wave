using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playersight : MonoBehaviour
{
   
    public Vector2 PointerPosition { get; set; }
    
    
    void Update()
    {
        transform.right = (PointerPosition - (Vector2)transform.position).normalized;
    }

   
}
