namespace Item
{
    public class Weapon : Item
    {
        public int Damage { get; private set; }
        public int Protection { get; }

        public Weapon()
        {
            Damage = 1;
            Protection = 0;
        }
        
        public void Reset()
        {
            Damage = 1;
        }

        public void Upgrade()
        {
            Damage += 10;
        }
    }
}