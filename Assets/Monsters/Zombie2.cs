using System;
using UnityEngine;

namespace Monsters
{
    public class Zombie2 : Zombie
    {
        public float playerdistance;
        public GameObject zombie2Projectile;
        public Transform launchOffset;
        private float _zombieWeaponRecharging =1 ;
        private float _bulletspeed;
        public Zombie2(float distance=3,float bulletspeed=1) :
            base("Zombie2",
                new[] { "Core" },
                50, 15, 100)
        {
            playerdistance = distance;
            _bulletspeed = bulletspeed;

        }

        protected override void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Playertarget = GameObject.FindWithTag("Player").transform;
            launchOffset = GameObject.FindWithTag("Zombie2LaunchOffset").transform;
        }

        protected override void Update()
        {
            Vector3 direction = Playertarget.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            Movement = direction;
            if (_zombieWeaponRecharging <= 0)
            {
                if ((Playertarget.position-transform.position).magnitude<playerdistance+2)
                {
                     GameObject t = Instantiate(zombie2Projectile, launchOffset.position, transform.rotation);
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
