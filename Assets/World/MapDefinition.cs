using DefaultNamespace;
using Random = System.Random;

namespace World
{
    public class MapDefinition
    {
        public TileDefinition[,] Map { get; }
        public int Height => Map.GetLength(0);
        public int Width => Map.GetLength(1);

        public MapDefinition()
        {
            Random rd = new Random(0);
            Map = new TileDefinition[100, 100];
            for (int i = 0; i < Width; i++)
            {
                Map[0,i] = new TileDefinition(EnumTile.GroundWhite);
                Map[Width-1, i] = new TileDefinition(EnumTile.GroundWhite);
                Map[i, 0] = new TileDefinition(EnumTile.GroundWhite);
                Map[i, Width - 1] = new TileDefinition(EnumTile.GroundWhite);
            }

            /*for (int i = 1; i < Height - 1; i++)
            {
                for (int j = 1; j < Width - 1; j++)
                {
                    Map[i, j] = new TileDefinition(rd.Next(1, 3));
                }
            }*/
        }
    }
}
