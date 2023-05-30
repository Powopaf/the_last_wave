using System;
using System.Collections;
using System.Linq;
using ATH.HealthBar;
using Pathfinding;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace IAPlayer
{
    public  class IAplayer : MonoBehaviour
    {
        public int health;
        public int Damage { get; private set; }
        public float speed;
        
        public Rigidbody2D rb;
        protected readonly string[] Target = new[] { "Zombie1", "Zombie3", "Zombie4", "Zombie5" };
        private (int, int) _coordinate;
        protected Vector2 Movement;
        public Animator animator;

        
        protected AIPath AI;
        public AIDestinationSetter AIsetter;
        
        private float _attackTimeCounter;
        private double _attackTime;
        public bool attacking;
        private bool _walking;
        private float _lastMoveX;
        private float _lastMoveY;

        private bool TargetZombie;
        private Transform coreTransform;


        protected void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            _attackTime = 0.5;
            attacking = false;

            health = 300;
            Damage = 100;

            AI = GetComponent<AIPath>();
            AIsetter = GetComponent<AIDestinationSetter>();
            
            coreTransform=GameObject.FindWithTag("Core").transform;
            AIsetter.target = coreTransform;
            TargetZombie = false;
        }

        protected void Update()
        {
            animator.SetFloat("X", Movement.x);
            animator.SetFloat("Y", Movement.y);
            /*if (attacking)
            {
                _attackTime -= Time.deltaTime;
                if (_attackTime <= 0)
                {
                    animator.SetBool("Attack", false);
                    _attackTime = 0.5;
                    attacking = false;
                }
            }*/

            if (!TargetZombie)
            {
                AIsetter.target = coreTransform;
            }

        }

        private void LateUpdate()
        {
            UpgradeDamage();
        }

        protected void FixedUpdate()
        { 
            Movement = AI.desiredVelocity;
            if (Movement != Vector2.zero)
            {
                animator.SetBool("Walking", true);
                _walking = true; 
                _lastMoveX = Movement.x; 
                _lastMoveY = Movement.y;
            }
            else if (_walking) 
            {
                animator.SetBool("Walking", false);
                animator.SetFloat("LastMoveX", _lastMoveX);
                animator.SetFloat("LastMoveY", _lastMoveY);
            }
        }

        private void UpgradeDamage()
        {
            StartCoroutine(TimerDamageUp());
        }

        private IEnumerator TimerDamageUp()
        {
            Damage += 2;
            health += 50;
           yield return new WaitForSeconds(120);
            
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (Target.Contains(col.tag) && !TargetZombie)
            {
                
                TargetZombie = true;
                AIsetter.target = col.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (AIsetter.target.CompareTag(other.tag))
            {
                TargetZombie = false;
            }
        }
        public bool ZombieDamageOnPlayer(int damage)
        {
            // ReSharper disable once IntDivisionByZero
            health -= damage;
            return health <= 0;
        }

       
    }
}

