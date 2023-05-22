using System.Collections.Generic;
using Item;

namespace LocalGame.LocalScript
{
    public class Inventory
    {
        private IItem[] Inv { get; }

        public Inventory()
        {
            Inv = new IItem[]
            {
                new Armor(),
                new Armor(),
                new Armor(),
                new Armor(),
                new Weapon()
            };
        }

        public int UpgradeItem(int money, ItemEnum item)
        {
            Dictionary<ItemEnum, int> index = new GetItem().GetInv;
            return Inv[index[item]].Upgrade(money);
        }
    }
}