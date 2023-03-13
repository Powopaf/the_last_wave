using System.Collections.Generic;

namespace World
{
    public class TileSprite
    {
        public IDictionary<EnumTile, int> Sprite { get; }

        public TileSprite()
        {
            Sprite = new Dictionary<EnumTile, int>
            {
                { EnumTile.GroundWhite, 0 },
                { EnumTile.GroundDirt, 1 },
                { EnumTile.GroundGrassMedium, 2 }
            };
        }
    }
}