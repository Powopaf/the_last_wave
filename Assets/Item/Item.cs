namespace Item
{
    public interface Item
    {
        public int Damage { get;}
        public int Protection { get;}
        public void Reset();
        public void Upgrade();
    }
}