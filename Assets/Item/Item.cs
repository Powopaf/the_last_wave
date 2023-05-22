namespace Item
{
    public interface IItem
    {
        public int Damage { get;}
        public int Protection { get;}
        public (int,int) Potion { get; }
        public void Reset();
        public bool Upgrade(int money);
    }
}