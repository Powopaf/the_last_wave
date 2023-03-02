using System;
using UnityEditor.SceneManagement;
using UnityEngine;


public class Map : MonoBehaviour
{
    public Tile[] tiles;
    private MapDefinition _mapDefinition;
    
    void Start()
    {
        _mapDefinition = new MapDefinition();
        SetUpTile();
    }

    void SetUpTile()
    {
        for (int i = 0; i < _mapDefinition.Height; i++)
        {
            for (int j = 0; j < _mapDefinition.Width; j++)
            {
                Tile t = tiles[_mapDefinition.Map[i, j]];
                Instantiate(t.visual, 
                    new Vector3(i, j, 0),
                    Quaternion.identity);
            }
        }
    }
}
