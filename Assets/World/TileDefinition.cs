using static World.GetType;

namespace World
{
    public class TileDefinition
    {
        public EnumTile TileType { get; set; }
        public bool HasLoot { get; } = false;
        public bool HaveSide { get; set; }
        public EnumTile[] Side = new EnumTile[4]; // top | right | bot | left
        public bool IsWall;
        public bool HaveProps { get; set; } = false;
        public (Obj, double) Prop;
        public bool HaveTree = false;
        public TileDefinition(EnumTile tile, bool haveSide = false)
        {
            TileType = tile;
            HaveSide = haveSide;
            IsWall = Walk(tile);
        }

        private bool Walk(EnumTile tile)
        {
            return tile == EnumTile.WallBorderMap || IsWater(tile);
        }
    }
}
