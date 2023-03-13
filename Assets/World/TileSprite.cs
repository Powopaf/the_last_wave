using System.Collections.Generic;
using DefaultNamespace;

namespace World
{
    public class TileSprite
    {
        public IDictionary<EnumTile, int> Sprite;

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