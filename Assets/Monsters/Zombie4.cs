using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Monsters
{
    public class Zombie4 : Zombie
    {
        public Zombie4() :
            base("Zombie4", 
                new []{"Player", "Core"},
                100, 85, 70) {}

        protected override void Awake()
        {
            throw new NotImplementedException();
        }
        protected override void Start()
        {
            throw new NotImplementedException();
        }

        protected override void FixedUpdate()
        {
            throw new NotImplementedException();
        }
        protected override void Update()
        {
            throw new NotImplementedException();
        }


        protected override void ZombieMovement(Vector2 direction)
        {
            throw new NotImplementedException();
        }
        
        protected override void OnCollisionEnter2D(Collision2D col)
        {
            throw new NotImplementedException();
        }
    }
}