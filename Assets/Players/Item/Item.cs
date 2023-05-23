namespace Players.Item
{
    public interface IItem
    {
        public int Damage { get;}
        public int Protection { get;}
        public (int,int) Potion { get; }
        public void Reset();
        public int Upgrade(int money);
    }
}