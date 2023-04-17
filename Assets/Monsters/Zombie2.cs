using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Monsters
{
    public class Zombie2 : Zombie
    {
        public float playerdistance;
        public GameObject zombie2Projectile;
        public GameObject launchOffset;
        private Rigidbody2D _launchOffsetRigidbody2D;
        private float _zombieWeaponRecharging =1 ;
        protected float _bulletspeed;
        private static readonly int X = Animator.StringToHash("X");
        private static readonly int Y = Animator.StringToHash("Y");

        public Zombie2(float bulletspeed=1) :
            base("Zombie2",
                new[] { "Survivor" },
                50, 15, 100)
        {
            _bulletspeed = bulletspeed;
            playerdistance = 10;

        }

        protected override void Awake()
        {
            playerdistance = 10;
            rb = GetComponent<Rigidbody2D>();
            launchOffset = GameObject.FindWithTag("Zombie2LaunchOffset");
            animator = GetComponent<Animator>();
            _launchOffsetRigidbody2D = launchOffset.GetComponent<Rigidbody2D>();
            AI=GetComponent<AIPath>();
            AIsetter = GetComponent<AIDestinationSetter>();
        }
        protected override void Start()
        {
        }

        protected override void Update()
        {
           /* AI.canMove = true;
            if (transform.position.magnitude<=playerdistance)
            {
                AI.canMove = false;
            }*/
            if (_zombieWeaponRecharging <= 0)
            {
                if ((AIsetter.target.position - transform.position).magnitude < playerdistance + 2)
                {
                     GameObject t = Instantiate(zombie2Projectile, launchOffset.transform.position, launchOffset.transform.rotation);
                     t.tag = "Zombie2Projectile";
                    _zombieWeaponRecharging = 1;
                }
            }
            else
            {
                _zombieWeaponRecharging -= Time.deltaTime;
            }
            Movement = AI.desiredVelocity;
            animator.SetFloat(X, Movement.x);
            animator.SetFloat(Y, Movement.y);
            
        }

        protected override void FixedUpdate()
        {
           
        }
        protected override void ZombieMovement(Vector2 direction)
        {
           
        }
        

        protected override void OnCollisionEnter2D(Collision2D col)
        {
            throw new NotImplementedException();
        }
    }
}
