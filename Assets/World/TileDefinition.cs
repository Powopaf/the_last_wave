namespace World
{
    public class TileDefinition
    {
        public int TileType { get; }
        public bool HasLoot = false;
        public bool GetHasLoot => HasLoot;
    
        public TileDefinition(int a)
        {
            TileType = a;
        }
    }
}
