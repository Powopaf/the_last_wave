using System.Linq;
using Pathfinding;
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

        protected abstract void Update();
        
        protected abstract void Start();

        protected abstract void FixedUpdate();
        protected abstract void ZombieMovement(Vector2 direction);

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (Target.Contains(other.tag))
            {
                AIsetter.target = other.transform;
            }
        }

        protected abstract void OnTriggerExit2D(Collider2D other);
        
    }
}
