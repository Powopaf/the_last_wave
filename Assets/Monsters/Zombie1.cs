using System.Linq;
using IAPlayer;
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


        public Zombie1() : base(new[] { "Assassin", "Farmer", "Survivor", "Worker", "Core", "IAplayer" }, 100, 20, 30)
        {
        }

        protected override void Awake()
        {
            lvl = GameObject.FindWithTag("ZombieLVL").GetComponent<ZombieLVL>().lvl;
            SetLevel();
            animator = GetComponent<Animator>();
            AI = GetComponent<AIPath>();
            AIsetter.target = GameObject.FindWithTag("Core").transform;
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
                else if (col.transform.CompareTag("Survivor"))
                {
                    var survivor = col.gameObject.GetComponent<Survivor>();
                    if (survivor.ZombieDamageOnPlayer(Damage)) // put Damage here
                    {
                        StartCoroutine(PlayerDeath(col, "Survivor"));
                    }
                }
                else if (col.transform.CompareTag("Worker"))
                {
                    var survivor = col.gameObject.GetComponent<Worker>();
                    if (survivor.ZombieDamageOnPlayer(Damage)) // put Damage here
                    {
                        StartCoroutine(PlayerDeath(col, "Worker"));
                    }
                }
                else if (col.transform.CompareTag("Assassin"))
                {
                    var survivor = col.gameObject.GetComponent<Assassin>();
                    if (survivor.ZombieDamageOnPlayer(Damage)) // put Damage here
                    {
                        StartCoroutine(PlayerDeath(col, "Assassin"));
                    }
                }
                else if (col.transform.CompareTag("IAplayer"))
                {
                    var survivor = col.gameObject.GetComponent<IAplayer>();
                    if (survivor.ZombieDamageOnPlayer(Damage)) // put Damage here
                    {
                        StartCoroutine(PlayerDeath(col, "IA Farmer"));
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
