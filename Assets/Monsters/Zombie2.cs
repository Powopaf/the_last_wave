using System;
using UnityEngine;

namespace Monsters
{
    public class Zombie2 : Zombie
    {
        public float playerdistance;

        public Zombie2(float distance=3) :
            base("Zombie2",
                new[] { "Core" },
                50, 15, 100)
        {
            playerdistance = distance;
        }

        protected override void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        protected override void Update()
        {
            Vector3 direction = Player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;
        }

        protected override void FixedUpdate()
        {
            ZombieMovement(movement);
        }
        protected override void ZombieMovement(Vector2 direction)
        {
            if ((Player.position-transform.position).magnitude > playerdistance)
            {
                rb.MovePosition((Vector2)transform.position + direction * (_speed * Time.deltaTime));
            }
        }

        protected override void OnCollisionEnter2D(Collision2D col)
        {
            throw new NotImplementedException();
        }
    }
}
