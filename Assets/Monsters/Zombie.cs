
using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Monsters
{
    public abstract class Zombie: MonoBehaviour
    {
        private int _health;
        protected int Damage;
        private string _name;
        //private Item[] _loot;
        protected string[] _target;
        private (int, int) _coordinate;
        public float speed;
        protected Transform Playertarget; //On doit pouvoir changer l'objet avec la fonction TargetZombie()
        public Rigidbody2D rb;
        protected Vector2 Movement;
        public Animator animator;
        protected AIPath AI;

        protected Zombie(string name = "", string[] target = null,
            int health = 1, int damage = 1, float speed = 1f)
        {
            _name = name;
            _target = target;
            _health = health;
            Damage = damage;
        }
        

        protected string TargetZombie()
        {
            string result = _target[0];
            for (int i = 1; i < _target.Length; i++)
            {
                Vector2 positionTest = GameObject.FindWithTag(_target[i]).transform.position;
                Vector2 positionResult = GameObject.FindWithTag(result).transform.position;
                float testmagnitude = ((Vector2) transform.position - positionTest).magnitude;
                float resultmagnitude = ((Vector2) transform.position - positionResult).magnitude;
                if (testmagnitude>resultmagnitude)
                {
                    result = _target[i];
                }
            }
            return result;
        }

        protected abstract void Awake();

        protected abstract void Update();
        
        protected abstract void Start();

        protected abstract void FixedUpdate();
        protected abstract void ZombieMovement(Vector2 direction);
       
        protected abstract void OnCollisionEnter2D(Collision2D col);
    }
}
