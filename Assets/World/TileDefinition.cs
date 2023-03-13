namespace World
{
    public class TileDefinition
    {
        public int TileType { get; }
        public bool GetHasLoot { get; } = false;

        public TileDefinition(int a)
        {
            TileType = a;
        }
    }
}
