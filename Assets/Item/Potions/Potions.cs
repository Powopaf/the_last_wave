namespace Item.Potions
{
    public class Potions : Item
    {
        private int _buff;

        public Potions(int durability = 1, int buff = 1)
        {
            _durability = durability;
            _buff = buff;
        }

        protected override void UpdateMe(int i)
        {
            _durability -= 1;
        }
    }
}
