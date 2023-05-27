using System.Collections;
using System.Linq;
using Pathfinding;
using UnityEngine;

namespace Monsters
{
    public class Zombie5 : Zombie
    {
        private static readonly int X = Animator.StringToHash("X");
        private static readonly int Y = Animator.StringToHash("Y");

        public Zombie5() : 
            base(new []{"Assassin","Farmer","Survivor","Worker"}, 
                10000, 100, 30) {}  
    
        protected override void Awake()
        {
            animator = GetComponent<Animator>();
            AI=GetComponent<AIPath>();
            AIsetter.target=GameObject.FindWithTag("Core").transform;

        }

        
       
        protected void Update()
        {
            Movement = AI.desiredVelocity;
            animator.SetFloat(X, Movement.x);
            animator.SetFloat(Y, Movement.y);
        }


        protected void OnCollisionStay2D(Collision2D col)
        {
            if (Target.Contains(col.transform.tag))   //Need to change  the tag
            {
                
            }
        
        }
        protected override void OnTriggerExit2D(Collider2D other)
        {
            if (Target.Contains(other.tag))
            {
                AIsetter.target = GameObject.FindWithTag("Core").transform;
            }
           
        }
    
    }
}

