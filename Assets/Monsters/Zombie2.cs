using System;
using UnityEngine;

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
        public Zombie2(float bulletspeed=1) :
            base("Zombie2",
                new[] { "Core" },
                50, 15, 100)
        {
            _bulletspeed = bulletspeed;
            playerdistance = 10;

        }

        protected override void Awake()
        {
            playerdistance = 10;
            rb = GetComponent<Rigidbody2D>();
            Playertarget = GameObject.FindWithTag("Player").transform;
            launchOffset = GameObject.FindWithTag("Zombie2LaunchOffset");
            animator = GetComponent<Animator>();
            _launchOffsetRigidbody2D = launchOffset.GetComponent<Rigidbody2D>();
        }
        protected override void Start()
        {
        }

        protected override void Update()
        {
            Vector3 direction = Playertarget.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _launchOffsetRigidbody2D.rotation = angle;
            direction.Normalize();
            Movement = direction;
            animator.SetFloat("X", Movement.x);
            animator.SetFloat("Y", Movement.y);
            if (_zombieWeaponRecharging <= 0)
            {
                if ((Playertarget.position - transform.position).magnitude < playerdistance + 2)
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
            
        }

        protected override void FixedUpdate()
        {
            ZombieMovement(Movement);
        }
        protected override void ZombieMovement(Vector2 direction)
        {
            if ((Playertarget.position-transform.position).magnitude > playerdistance)
            {
                rb.MovePosition((Vector2)transform.position + direction * (speed * Time.deltaTime));
            }
        }

        protected override void OnCollisionEnter2D(Collision2D col)
        {
            throw new NotImplementedException();
        }
    }
}
