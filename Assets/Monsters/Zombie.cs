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
        protected int Damage;
        private string _name;
        //private Item[] _loot;
        protected readonly string[] Target;
        private (int, int) _coordinate;
        public float speed;
        protected Transform Playertarget;
        public Rigidbody2D rb;
        protected Vector2 Movement;
        public Animator animator;
        protected AIPath AI;
        public AIDestinationSetter AIsetter;

        protected Zombie(string name = "", string[] target = null,
            int health = 1, int damage = 1, float speed = 1f)
        {
            _name = name;
            Target = target; 
            Health = health;
            Damage = damage;
        }
        

        protected string TargetZombie()
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
        }

        protected abstract void Awake();


        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (Target.Contains(other.tag))
            {
                AIsetter.target = other.transform;
            }
        }

        protected abstract void OnTriggerExit2D(Collider2D other);

        // ReSharper disable Unity.PerformanceAnalysis
        protected IEnumerator PlayerDeath(Collision2D col, string id)
        {
            if (id == "Farmer")
            {
                GameObject o = col.gameObject;
                o.tag = "Dead"; 
                var rbPlayer = o.GetComponent<Rigidbody2D>();
                o.GetComponent<SpriteRenderer>().sortingLayerName = "PlayerDeath";
                rbPlayer.constraints = RigidbodyConstraints2D.FreezePosition;
                yield return new WaitForSeconds(10);
                var h = o.GetComponent<Farmer>();
                h.Health = h.MaxHealth;
                o.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                o.tag = "Farmer";
                rbPlayer.constraints = RigidbodyConstraints2D.None;
                // ReSharper disable once Unity.InefficientPropertyAccess
                rbPlayer.constraints = RigidbodyConstraints2D.FreezeRotation;

            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected IEnumerator CooldownAttack(int time = 2)
        {
            yield return new WaitForSeconds(time);
        }
    }
}
