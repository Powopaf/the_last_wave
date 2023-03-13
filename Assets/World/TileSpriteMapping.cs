using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class TileSpriteMapping : MonoBehaviour
    {
        public IDictionary<TileType, int> Sprites { get; } = new Dictionary<TileType, int>
        {
            { TileType.GroundWhite, 0 },
            { TileType.GroundDirt, 1 },
            { TileType.GroundGrassLight, 2 }
        };
    }
}
