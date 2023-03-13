using DefaultNamespace;

namespace World
{
    public class TileDefinition
    {
        public int TileType { get; }
        public bool GetHasLoot { get; } = false;

        public TileDefinition(EnumTile tile)
        {
            var tileSprite = new TileSprite();
            TileType = tileSprite.Sprite[tile];
        }
    }
}
