using System.Linq;
using Pathfinding;
using Photon.Pun;
using Players;
using UnityEngine;

namespace Monsters
{
    public class Zombie1 : Zombie
    {
        private static readonly int X = Animator.StringToHash("X");
        private static readonly int Y = Animator.StringToHash("Y");


        public Zombie1() : 
            base("Zombie1", new []{"Assassin","Farmer","Survivor","Worker"}, 100, 20, 30) { }

        
        protected override void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            AI = GetComponent<AIPath>();
            AIsetter.target=GameObject.FindWithTag("Farmer").transform;
        }


        protected void Update()
        { 
            Movement = AI.desiredVelocity;
            animator.SetFloat(X, Movement.x);
            animator.SetFloat(Y, Movement.y);
        }


        protected void OnCollisionStay2D(Collision2D col)
        {
            if (Target.Contains(col.transform.tag)) //Need to add tag
            {
                if (col.transform.CompareTag("Farmer"))
                {
                    Farmer survivor = col.gameObject.GetComponent<Farmer>();
                    if (survivor.ZombieDamageOnPlayer(5)) // put Damage here
                    {
                        StartCoroutine(PlayerDeath(col, "Farmer"));
                    }
                }
            }
            StartCoroutine(CooldownAttack(5));
        }
        
        

        protected override void OnTriggerExit2D(Collider2D other)
        {
            if (Target.Contains(other.tag))
            {
                AIsetter.target = GameObject.FindWithTag("Core").transform;
            }
        }

        public bool ZombieTakeDamage(int damage)
        {
            Health -= damage;
            return Health <= 0;
        }
    }
}
