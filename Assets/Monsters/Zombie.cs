using System;
using UnityEngine;

namespace Monsters
{
    public abstract class Zombie: MonoBehaviour
    {
        private int _health;
        protected int Damage;
        private string _name;
        //private Item[] _loot;
        private string[] _target;
        private (int, int) _coordinate;
        public float speed;
        protected Transform Playertarget; //On doit pouvoir changer l'objet avec la fonction TargetZombie()
        public Rigidbody2D rb;
        protected Vector2 Movement;
        public Animator animator;

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
            Vector3 closertargetPosition = GameObject.Find(_target[0]).transform.position;
            string result = _target[0];
            for (int i = 1; i < _target.Length; i++)
            {
                Vector3 newtargetPosition = GameObject.Find(_target[i]).transform.position;
                Vector3 position = transform.position;
                Vector3 firstcomparison = position - closertargetPosition;
                Vector3 secondcomparison = position - newtargetPosition;
                if (secondcomparison.magnitude < firstcomparison.magnitude)
                {
                    closertargetPosition = newtargetPosition;
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
