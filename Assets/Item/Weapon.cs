namespace Item
{
    public class Weapon : IItem
    {
        public int Damage { get; private set; }
        public int Protection => 0;
        
        public (int, int) Potion => (-1, -1);

        public Weapon()
        {
            Damage = 1;
        }
        
        public void Reset()
        {
            Damage = 1;
        }

        public bool Upgrade(int money)
        {
            if (money >= 10)
            {
                Damage += 10;
                return true;
            }
            return false;
        }
    }
}