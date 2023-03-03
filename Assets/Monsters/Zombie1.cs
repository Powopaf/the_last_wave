using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie1 : Zombie
{
    public Zombie1() : 
        base("Zombie1", new []{"Player", "Core"}, 
        100, 20, 30) {}

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Players.Player survivor = GetComponent<Players.Survivor>();
            survivor.TakeDamage(_damage);
            
        }
    }

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
        Vector3 direction = playerobject.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }
    protected override void FixedUpdate()
    {
        ZombieMovement(movement);
    }
}
