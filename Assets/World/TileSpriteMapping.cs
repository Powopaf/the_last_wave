using System.Collections.Generic;
using UnityEngine;

public class TileSpriteMapping : MonoBehaviour
{
    public Tile[] tiles;
    public IDictionary<TileType, int> Sprites { get; } = new Dictionary<TileType, int>
    {
        { TileType.GroundBlack, 0 },
        { TileType.GroundDirt, 1 },
        { TileType.GroundGrassLight, 2 }
    };
}
