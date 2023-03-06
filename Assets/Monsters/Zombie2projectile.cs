using System;
using System.Collections;
using System.Collections.Generic;
using Monsters;
using UnityEngine;

public class Zombie2projectile : Zombie2
{
    public float bulletspeed;

    public Zombie2projectile(float speed)
    {
        bulletspeed = speed;
    }
    new void  Update()
     {
         transform.position += transform.right * (Time.deltaTime * bulletspeed);
         
     }

     new void OnCollisionEnter2D(Collision2D col)
     {
         if (col.transform.CompareTag("Player"))
         {
             Players.Player player = Playertarget.transform.GetComponent<Players.Player>(); //Zombie Attack
             player.ZombieDamageOnPlayer(_damage); // Zombie Attack 
         }
         Destroy(gameObject);
     }
}
