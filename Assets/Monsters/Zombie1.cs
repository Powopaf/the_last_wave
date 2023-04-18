using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using Players;
using UnityEngine;
using UnityEngine.InputSystem;
using World;

namespace Monsters
{
    public class Zombie1 : Zombie
    {
        private static readonly int X = Animator.StringToHash("X");
        private static readonly int Y = Animator.StringToHash("Y");


        public Zombie1() : 
            base("Zombie1", new []{"Assassin","Farmer","Survivor","Worker"}, 
                100, 20, 30) {}

        
        protected override void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            AI=GetComponent<AIPath>();
            AIsetter.target=GameObject.FindWithTag("Core").transform;

        }
        protected override void Start()
        {
           
        }
       

        protected override void ZombieMovement(Vector2 direction)
        {
        }
        
       
        protected override void Update()
        {
            Movement = AI.desiredVelocity;
           animator.SetFloat(X, Movement.x);
           animator.SetFloat(Y, Movement.y);
        }
        protected override void FixedUpdate()
        {
        }

       
        protected override void OnCollisionEnter2D(Collision2D col)
        {
            if ( _target.Contains(col.transform.tag))   //Need to change  the tag
            {
                Players.Survivor survivor = Playertarget.transform.GetComponent<Players.Survivor>(); //Zombie Attack
                survivor.ZombieDamageOnPlayer(Damage); // Zombie Attack
            }
        
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            if (_target.Contains(other.tag))
            {
                AIsetter.target = GameObject.FindWithTag("Core").transform;
            }
           
        }
    }
}
