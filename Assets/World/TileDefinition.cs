using Unity.VisualScripting;

namespace World
{
    public class TileDefinition
    {
        public EnumTile TileType { get; set; }
        public bool HasLoot { get; } = false;
        public bool HasSide { get; set; }
        public EnumTile[] Side = new EnumTile[4];
        public bool IsWall;

        public TileDefinition(EnumTile tile, bool hasSide = false)
        {
            TileType = tile;
            HasSide = hasSide;
            IsWall = Walk(tile);
        }

        private bool Walk(EnumTile tile)
        {
            return tile switch
            {
                EnumTile.WallBorderMap => false,
                EnumTile.Water1 => false,
                _ => true
            };
        }
    }
}
