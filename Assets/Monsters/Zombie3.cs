using System;
using System.Linq;
using Pathfinding;
using Photon.C__script;
using Photon.Pun;
using Players;
using UnityEngine;

namespace Monsters
{
    public class Zombie3 : Zombie
    {
        private static readonly int X = Animator.StringToHash("X");
        private static readonly int Y = Animator.StringToHash("Y");

        public Zombie3() :
            base(new string[] { "PLayerWall", "Core", "Turret" }, 200, 15, 10) { }

        protected override void Awake()
        {
            lvl = GameObject.FindWithTag("ZombieLVL").GetComponent<ZombieLVL>().lvl;
            SetLevel();
            animator = GetComponent<Animator>();
            AI = GetComponent<AIPath>();
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
            
            if (Target.Contains(col.transform.tag) && CanAttack) //Need to add tag
            {
                if (col.transform.CompareTag("PlayerWall"))
                {
                    var wall = col.gameObject.GetComponent<Wall>();
                    if (wall.DamageWall(Damage)) // put Damage here
                    {
                        PhotonNetwork.Destroy(col.gameObject);
                    }
                }
                else if (col.transform.CompareTag("Turret"))
                {
                    var turret = col.gameObject.GetComponent<Turret>();
                    if (turret.DamageTurret(Damage))
                    {
                        PhotonNetwork.Destroy(col.gameObject);
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