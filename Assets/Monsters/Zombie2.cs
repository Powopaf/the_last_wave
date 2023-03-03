using System;
using UnityEngine;

namespace Monsters
{
    public class Zombie2 : Zombie
    {
        public Zombie2() :
            base("Zombie2",
                new[] { "Core" },
                50, 15, 100)
        {}

       

        protected override void Start()
        {
            throw new NotImplementedException();
        }

        protected override void Update()
        {
            throw new NotImplementedException();
        }

        protected override void FixedUpdate()
        {
            throw new NotImplementedException();
        }
        protected override void ZombieMovement(Vector2 direction)
        {
            throw new NotImplementedException();
        }
    }
}
