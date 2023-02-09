using System;

namespace Monsters
{
    public class Zombie2 : Zombie
    {
        public Zombie2() :
            base("Zombie2", 
                new []{"Core"},
                50, 15, 100) {}

        private static void Movement()
        {
            throw new NotImplementedException();
        }
    }
}
