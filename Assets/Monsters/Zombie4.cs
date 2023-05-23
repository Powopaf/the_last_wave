using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Monsters
{
    public class Zombie4 : Zombie
    {
        private static readonly int X = Animator.StringToHash("X");
        private static readonly int Y = Animator.StringToHash("Y");

        public Zombie4() :
            base("Zombie4",
                new[] { "Player", "Core" },
                100, 85, 70)
        {
        }

        protected override void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            AI = GetComponent<AIPath>();
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
            if (((IList<string>)_target).Contains(col.transform.tag)) //Need to change  the tag
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