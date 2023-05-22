using System;

namespace Item
{
    public class Potions : IItem
    {
        public int Damage => 0;
        public int Protection => 0;
        public (int, int) Potion { get; } // (level of the potion, healing amount)

        public Potions()
        {
            Potion = (0, 5);
        }

        public Potions(int level)
        {
            Potion = level switch
            {
                > 2 => throw new ArgumentException("level of potion is to high must be 0 <= level <= 2"),
                0 => (level, 5),
                1 => (level, 10),
                _ => (level, 15)
            };
        }

        public Potions(int level, int healing)
        {
            Potion = (level, healing);
        }
        
        public void Reset() { }

        public int Upgrade(int money)
        {
            return money;
        }
    }
}