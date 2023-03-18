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
                { EnumTile.GroundWhite, 0 },
                { EnumTile.GroundDirt, 1 },
                { EnumTile.GroundGrassMedium, 2 }
            };

            Tiles = new Dictionary<int, EnumTile>
            {
                { 0, EnumTile.GroundWhite },
                { 1, EnumTile.GroundDirt },
                { 2, EnumTile.GroundGrassMedium }
            };
        }
    }
}