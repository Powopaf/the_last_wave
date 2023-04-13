using UnityEngine;
using System.Linq;

namespace World
{
    public class Map : MonoBehaviour
    {
        public Tile[] tiles;
        public Side[] side;
        private MapDefinition _mapDefinition;
        private readonly TileSprite _tileSprite = new TileSprite();
        void Start()
        {
            _mapDefinition = new MapDefinition();
            //SetMapGen();
            SetUpTile();
        }

        private void SetMapGen() // don't work
        {
            Instantiate(Resources.Load(@"Assets/Resources/MapGenerator"));
        }
        
        private void SetUpTile()
        {
            for (int i = 0; i < _mapDefinition.Height; i++)
            {
                for (int j = 0; j < _mapDefinition.Width; j++)
                {
                    TileDefinition current = _mapDefinition.Map[i, j];
                    var tile = tiles[_tileSprite.Sprite[current.TileType]];
                    GameObject go = Instantiate(tile.visual, new Vector3(i, j, 0), Quaternion.identity);
                    if (!tile.iswalkable)
                    {
                        go.AddComponent<BoxCollider2D>();
                    }
                    if (current.HaveSide)
                    {
                        for (var index = 0; index < current.Side.Length; index++)
                        {
                            var t = current.Side[index];
                            if (t != EnumTile.NoTile)
                            {
                                var s = side[_tileSprite.Side[t]];
                                if (index == 0) // top Side
                                {
                                    Instantiate(s.visual, new Vector3(i, j + 1, -0.5f), Quaternion.identity);
                                }
                                else if (index == 1) // Right side
                                {
                                    Instantiate(s.visual, new Vector3(i + 1, j, -0.5f), Quaternion.identity);
                                }
                                else if (index == 2) // bottom side
                                {
                                    Instantiate(s.visual, new Vector3(i, j - 1, -0.5f), Quaternion.identity);
                                }
                                else
                                {
                                    Instantiate(s.visual, new Vector3(i - 1, j, -0.5f), Quaternion.identity);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}