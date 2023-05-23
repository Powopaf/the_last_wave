using System.Collections.Generic;

namespace Players.Inventory
{
    public enum ItemEnum
    {
        Helmet,
        ChestPlate,
        Gloves,
        Boots,
        Sword
    }

    public class GetItem
    {
        public Dictionary<ItemEnum, int> GetInv { get; }

        public GetItem()
        {
            GetInv = new Dictionary<ItemEnum, int>
            {
                { ItemEnum.Helmet, 0 },
                { ItemEnum.ChestPlate, 1 },
                { ItemEnum.Gloves, 2 },
                { ItemEnum.Boots, 3 },
                { ItemEnum.Sword, 4 }
            };
        }
    }
}