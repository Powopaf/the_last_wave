using UnityEngine;

namespace World
{
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
                    var tile = tiles[_mapDefinition.Map[i,j].TileType];
                    GameObject go = Instantiate(tile.visual, new Vector3(i, j, 0), Quaternion.identity);
                    if (!tile.iswalkable)
                    {
                        go.AddComponent<BoxCollider2D>();
                    }
                }
            }
        }
    }
}