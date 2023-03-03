using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1 : Zombie
{
    public Zombie1() : 
        base("Zombie1", new []{"Player", "Core"}, 
        100, 20, 30) {}

    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected  override void ZombieMovement(Vector2 direction)
    {
        rb.MovePosition((Vector2) transform.position +direction * (_speed * Time.deltaTime));
    }

    protected override void Update()
    {
        Vector3 direction = Player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }
    protected override void FixedUpdate()
    {
        ZombieMovement(movement);
    }
    protected override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))   //Need to change  the tag
        {
            Players.Survivor survivor = Player.transform.GetComponent<Players.Survivor>(); //Zombie Attack
            survivor.ZombieDamageOnPlayer(_damage); // Zombie Attack
        }
        
    }
}
