using System.Collections;
using System.Collections.Generic;
using Players;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = new GameObject();
        player.name = "test";
        player.AddComponent<Rigidbody2D>();
        player.AddComponent<BoxCollider2D>();
        player.
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
