using Random = System.Random;

public class MapDefinition
{
    public int[,] Map { get; }
    public int Height => Map.GetLength(0);
    public int Width => Map.GetLength(1);

    public MapDefinition()
    {
        Random rd = new Random(0);
        Map = new int[100, 100];
        for (int i = 0; i < Width; i++)
        {
            Map[0,i] = 13;
            Map[Width-1, i] = 13;
            Map[i, 0] = 13;
            Map[i, Width - 1] = 13;
        }

        for (int i = 1; i < Height - 1; i++)
        {
            for (int j = 1; j < Width - 1; j++)
            {
                Map[i, j] = rd.Next(0,13);
            }
        }
    }
}
