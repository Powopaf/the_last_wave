namespace Item
{
    public class Armor : Item
    {
        public int Damage { get; }
        public int Protection { get; private set; }

        public Armor()
        {
            Damage = 0;
            Protection = 1;
        }
        public void Reset()
        {
            Protection = 0;
        }

        public void Upgrade()
        {
            Protection += 10;
        }
    }
}