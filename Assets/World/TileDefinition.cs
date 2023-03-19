using Unity.VisualScripting;

namespace World
{
    public class TileDefinition
    {
        public EnumTile TileType { get; }
        public bool HasLoot { get; } = false;
        public bool HasSide { get; set; }
        public EnumTile[] Side = new EnumTile[4];

        public TileDefinition(EnumTile tile, bool hasSide = false)
        {
            TileType = tile;
            HasSide = hasSide;
        }
    }
}
