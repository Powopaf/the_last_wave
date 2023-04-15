using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;
using World;

namespace Monsters
{
    public class Zombie1 : Zombie
    {
        public Zombie1() : 
            base("Zombie1", new []{"Player", "Core"}, 
                100, 20, 30) {}

        
        protected override void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            Playertarget = GameObject.FindWithTag("Player").transform;
            animator = GetComponent<Animator>();
            Movement= GetComponent<AIDestinationSetter>().

        }
        protected override void Start()
        {
           
        }
       

        protected override void ZombieMovement(Vector2 direction)
        {
            //rb.MovePosition((Vector2)transform.position + direction * (speed * Time.deltaTime));
        }
        
        protected override void Update()
        { 
           
            //Vector3 direction = Playertarget.position - transform.position;
           // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           // rb.rotation = angle;
            //direction.Normalize();
           // Movement = direction;
           Vector3 horizontalMove=
            animator.SetFloat("X", transform.position.x);
            animator.SetFloat("Y", transform.position.y);
        }
        protected override void FixedUpdate()
        {
            //ZombieMovement(Movement);
        }

       
        protected override void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.CompareTag("Player"))   //Need to change  the tag
            {
                Players.Survivor survivor = Playertarget.transform.GetComponent<Players.Survivor>(); //Zombie Attack
                survivor.ZombieDamageOnPlayer(Damage); // Zombie Attack
            }
        
        }
    
    }
}
