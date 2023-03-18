using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playersight : MonoBehaviour
{
    private GameObject LaunchOffsetPlayer;
    private Rigidbody2D RblaunchOffsetPLayer;
   
    private InputAction _sightmove;
    public Vector2 PointerPosition { get; set; }


    void Awake()
    {
        LaunchOffsetPlayer = GameObject.FindWithTag("PlayerLaunchOffset");
        RblaunchOffsetPLayer = LaunchOffsetPlayer.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        transform.right = (PointerPosition - (Vector2)transform.position).normalized;
    }

   
}
