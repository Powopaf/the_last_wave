using System;
using System.Collections;
using System.Collections.Generic;
using Monsters;
using UnityEngine;

public class Zombie2projectile : MonoBehaviour
{
    public float bulletspeed;

    public Zombie2projectile(float speed)
    {
        bulletspeed = speed;
    }
    void  Update()
     {
         transform.position += -transform.right * (Time.deltaTime * bulletspeed);
     }

     void OnCollisionEnter2D(Collision2D col)
     {
         Destroy(gameObject);
     }
}
