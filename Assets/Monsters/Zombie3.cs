using System;
using UnityEngine;

namespace Monsters
{
    public class Zombie3 : Zombie
    {
        public Zombie3() :
            base("Zombie3", 
                new []{"Building", "Core"},
                200, 15, 10) {}
        
        
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
