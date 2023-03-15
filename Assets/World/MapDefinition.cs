namespace World
{
    public class MapDefinition
    {
        public TileDefinition[,] Map { get; }
        public int Height => Map.GetLength(0);
        public int Width => Map.GetLength(1);

        public MapDefinition()
        {
            Map = new TileDefinition[50, 25];
            for (int i = 0; i < Width; i++)
            {
                Map[0, i] = new TileDefinition(EnumTile.GroundWhite);
                Map[Height - 1, i] = new TileDefinition(EnumTile.GroundWhite);
            }
            for (int j = 0; j < Height; j++)
            {
                Map[j, 0] = new TileDefinition(EnumTile.GroundWhite);
                Map[j, Width - 1] = new TileDefinition(EnumTile.GroundWhite);
            }
            //SeedMap();
        }

        private void DefaultMap()
        {
            System.Random rd = new System.Random(0);
            for (int i = 1; i < Height - 1; i++)
            {
                for (int j = 1; j < Width - 1; j++)
                {
                    int a = rd.Next(1, 3);
                    var tile = a == 1 ? EnumTile.GroundDirt : EnumTile.GroundGrassMedium;
                    Map[i, j] = new TileDefinition(tile);
                }
            }
        }

        private void SeedMap(int n = 1)
        {
            SeedGeneration seed = new SeedGeneration();
            seed.GenerateSeeds(n);
            for (int i = 0; i < Height - 1; i++)
            {
                for (int j = 0; j < Width - 1; j++)
                {
                    Map[i, j] = new TileDefinition(seed.Distance(i, j));
                }
            }
        }
    }
}
