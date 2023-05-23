namespace Item
{
    public class Armor : IItem
    {
        public int Damage => 0;
        public int Protection { get; private set; }
        public (int, int) Potion => (-1, -1);

        public Armor()
        {
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