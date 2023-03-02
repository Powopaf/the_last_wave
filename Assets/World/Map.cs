using UnityEngine;


public class Map : MonoBehaviour
{
    private Tile[,] map;
    private int Height;
    private int Width;

    void Start()
    {
        //Set up map
    }

    void SetUpTile(string file)
    {
        
    }
    
    void SetUpMap()
    {
        map = new Tile[0,0];
        Height = map.GetLength(0);
        Width = map.GetLength(1);
    }

    void RenderMap()
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                
            }
        }
    }
}