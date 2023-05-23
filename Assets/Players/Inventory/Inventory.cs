using System.Collections.Generic;
using Players.Item;

namespace Players.Inventory
{
    public class Inventory
    {
        public (IItem, int)[] Inv { get; }

        public Inventory()
        {
            Inv = new (IItem, int)[]
            {
                (new Armor(), 1),
                (new Armor(), 1),
                (new Armor(), 1),
                (new Armor(), 1),
                (new Weapon(), 1)
            };
        }

        public int UpgradeItem(int money, ItemEnum item)
        {
            Dictionary<ItemEnum, int> index = new GetItem().GetInv;
            int m = Inv[index[item]].Item1.Upgrade(money);
            if (m == money)
            {
                return money;
            }

            Inv[index[item]].Item2 += 1;
            return m;
        }
    }
}