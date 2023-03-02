public class MapDefinition
{
    public int[,] Map { get; }
    public int Height => Map.GetLength(0);
    public int Width => Map.GetLength(1);

    public MapDefinition()
    {
        Map = new int[,]
        {
            { 1, 1, 1, 1 },
            { 1, 0, 0, 1 },
            { 1, 1, 1, 1 },
        };
    }
}
