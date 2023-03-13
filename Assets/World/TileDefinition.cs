namespace World
{
    public class TileDefinition
    {
        public EnumTile TileType { get; }
        public bool GetHasLoot { get; } = false;

        public TileDefinition(EnumTile tile)
        {
            TileType = tile;
        }
    }
}
