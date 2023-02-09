using System;

namespace Monsters
{
    public partial class Zombie3 : Zombie
    {
        public Zombie3() :
            base("Zombie3", 
                new []{"Building", "Core"},
                200, 15, 10) {}

        private static void Movement()
        {
            throw new NotImplementedException();
        }
    }
}
