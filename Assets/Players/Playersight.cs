using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playersight : MonoBehaviour
{
    private GameObject LaunchOffsetPlayer;
    private Rigidbody2D RblaunchOffsetPLayer;
    [SerializeField] protected new Camera camera;
    
    void Start()
    {
        LaunchOffsetPlayer = GameObject.FindWithTag("PlayerLaunchOffset");
        RblaunchOffsetPLayer = LaunchOffsetPlayer.GetComponent<Rigidbody2D>();
        camera=Camera.main;
    }

   
    void Update()
    {
        Vector3 mousepos = Input.mousePosition;
        mousepos.z = camera.nearClipPlane;
        Vector3 worldpmousepos = camera.ScreenToWorldPoint(mousepos);
        Vector3 direction = worldpmousepos - LaunchOffsetPlayer.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        RblaunchOffsetPLayer.rotation = angle;
    }
}
