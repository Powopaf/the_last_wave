using UnityEngine;

namespace World
{
    public class Map : MonoBehaviour
    {
        public Tile[] tiles;
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
                        foreach (EnumTile side in current.Side)
                        {
                            if (side != EnumTile.NoTile)
                            {
                                
                            }
                        }
                    }
                }
            }
        }
    }
}