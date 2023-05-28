using System;
using System.Collections;
using System.Linq;
using Pathfinding;
using Players;
using UnityEngine;

namespace Monsters
{
    public abstract class Zombie: MonoBehaviour
    {
        protected int Health;
        protected readonly int Damage;
        protected readonly string[] Target;
        private (int, int) _coordinate;
        protected Vector2 Movement;
        public Animator animator;
        protected AIPath AI;
        public AIDestinationSetter AIsetter;
        protected bool CanAttack = true;

        protected Zombie(string[] target = null,
            int health = 1, int damage = 1, float speed = 1f)
        {
            Target = target; 
            Health = health;
            Damage = damage;
        }
        

        /*protected string TargetZombie()
        {
            string result = Target[0];
            for (int i = 1; i < Target.Length; i++)
            {
                Vector2 positionTest = GameObject.FindWithTag(Target[i]).transform.position;
                Vector2 positionResult = GameObject.FindWithTag(result).transform.position;
                float testmagnitude = ((Vector2) transform.position - positionTest).magnitude;
                float resultmagnitude = ((Vector2) transform.position - positionResult).magnitude;
                if (testmagnitude>resultmagnitude)
                {
                    result = Target[i];
                }
            }
            return result;
        }*/

        protected abstract void Awake();


        protected void OnTriggerStay2D(Collider2D other)
        {
            if (Target.Contains(other.tag))
            {
                AIsetter.target = other.transform;
            }
            if (AIsetter.target.CompareTag("Dead"))
            {
                AIsetter.target=GameObject.FindWithTag("Core").transform;
            }
        }

        protected void OnTriggerExit2D(Collider2D other)
        {
            if (Target.Contains(other.tag))
            {
                AIsetter.target = GameObject.FindWithTag("Core").transform;
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected IEnumerator PlayerDeath(Collision2D col, string id)
        {
            if (id == "Farmer")
            {
                // just die
                GameObject farmer = col.gameObject;
                farmer.tag = "Dead";
                var rbPlayer = farmer.GetComponent<Rigidbody2D>();
                farmer.GetComponent<SpriteRenderer>().sortingLayerName = "PlayerDeath";
                rbPlayer.constraints = RigidbodyConstraints2D.FreezePosition;

                yield return new WaitForSeconds(10); // is dead
                
                // back to life
                var h = farmer.GetComponent<Farmer>();
                h.Health = h.MaxHealth;
                farmer.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                farmer.tag = "Farmer";
                rbPlayer.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        protected IEnumerator DelayAttack(int time = 3)
        {
            CanAttack = false;
            yield return new WaitForSeconds(time);
            CanAttack = true;
        }
        
        public bool ZombieTakeDamage(int damage)
        {
            Health -= damage;
            return Health <= 0;
        }
    }
}
