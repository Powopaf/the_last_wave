using System.Linq;
using Pathfinding;
using Photon.C__script;
using Players;
using UnityEngine;

namespace Monsters
{
    public class Zombie1 : Zombie
    {
        private static readonly int X = Animator.StringToHash("X");
        private static readonly int Y = Animator.StringToHash("Y");


        public Zombie1() : base(new [] {"Assassin","Farmer","Survivor","Worker"}, 100, 20, 30) { }

        protected override void Awake()
        {
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
            if (Target.Contains(col.transform.tag) && CanAttack) //Need to add tag
            {
                if (col.transform.CompareTag("Farmer"))
                {
                    Farmer survivor = col.gameObject.GetComponent<Farmer>();
                    if (survivor.ZombieDamageOnPlayer(Damage)) // put Damage here
                    {
                        StartCoroutine(PlayerDeath(col, "Farmer"));
                    }
                }
                else
                {
                    col.gameObject.GetComponent<Crystal>().AttackCrystal(Damage);
                }
                StartCoroutine(DelayAttack());
            }
        }
    }
}
