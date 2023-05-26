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
            AIsetter.target=GameObject.FindWithTag("Survivor").transform;

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
            
            if (Target.Contains(col.transform.tag))   //Need to change  the tag
            {
                if (col.transform.CompareTag("Survivor"))
                {
                    Survivor survivor = col.transform.GetComponent<Survivor>();
                    survivor.ZombieDamageOnPlayer(Damage);
                }
            }
        
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
