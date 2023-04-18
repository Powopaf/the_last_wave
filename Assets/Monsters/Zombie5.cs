using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

namespace Monsters
{
    public class Zombie5 : Zombie
    {
        private static readonly int X = Animator.StringToHash("X");
        private static readonly int Y = Animator.StringToHash("Y");

        public Zombie5() : 
            base("Zombie1", new []{"Assassin","Farmer","Survivor","Worker"}, 
                10000, 100, 30) {}  
    
        protected override void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            AI=GetComponent<AIPath>();

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
            if ( ((IList)_target).Contains(col.transform.tag))   //Need to change  the tag
            {
                Players.Survivor survivor = Playertarget.transform.GetComponent<Players.Survivor>(); //Zombie Attack
                survivor.ZombieDamageOnPlayer(Damage); // Zombie Attack
            }
        
        }
        protected override void OnTriggerExit2D(Collider2D other)
        {
            throw new NotImplementedException();
        }
    
    }
}

