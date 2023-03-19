using System.Collections.Generic;

namespace World
{
    public class TileSprite
    {
        public IDictionary<EnumTile, int> Sprite { get; }
        public IDictionary<int, EnumTile> Tiles { get; }

        public TileSprite()
        {
            Sprite = new Dictionary<EnumTile, int>
            {
                { EnumTile.WallBorderMap, 0 },
                { EnumTile.Dirt1, 1 },
                { EnumTile.Grass1, 2 },
                { EnumTile.Sand1, 3 },
                { EnumTile.Snow1, 4 },
                { EnumTile.Water1, 5 }
            };

            Tiles = new Dictionary<int, EnumTile>
            {
                { 0, EnumTile.WallBorderMap },
                { 1, EnumTile.Dirt1 },
                { 2, EnumTile.Grass1 },
                { 3, EnumTile.Sand1 },
                { 4, EnumTile.Snow1 },
                { 5, EnumTile.Water1 }
            };
        }
    }
}