using UnityEngine;

namespace World
{
    public class Map : MonoBehaviour
    {
        public Tile[] tiles;
        private MapDefinition _mapDefinition;
        private TileSprite _tileSprite = new TileSprite();
    
        void Awake()
        {
            _mapDefinition = new MapDefinition();
            //SetMapGen();
            SetUpTile();
        }

        private void SetMapGen() // don't work
        {
            Instantiate(Resources.Load(@"Assets/Resources/MapGenerator.prefab"));
        }
        
        private void SetUpTile()
        {
            for (int i = 0; i < _mapDefinition.Height; i++)
            {
                for (int j = 0; j < _mapDefinition.Width; j++)
                {
                    var tile = tiles[_tileSprite.Sprite[_mapDefinition.Map[i, j].TileType]];
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