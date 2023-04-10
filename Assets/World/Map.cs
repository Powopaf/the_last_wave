using UnityEngine;

namespace World
{
    public class Map : MonoBehaviour
    {
        public Tile[] tiles;
        private MapDefinition _mapDefinition;
        private TileSprite _tileSprite = new TileSprite();
    
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
                    var tile = tiles[_tileSprite.Sprite[_mapDefinition.Map[i, j].TileType]];
                    GameObject go = Instantiate(tile.visual, new Vector3(i, j, 0), Quaternion.identity);
                    if (!tile.iswalkable)
                    {
                        go.AddComponent<BoxCollider2D>();
                    }
                    if (_mapDefinition.Map[i,j].HasSide)
                    {
                        EnumTile[] side = _mapDefinition.Map[i, j].Side;
                        for (var index = 0; index < side.Length; index++)
                        {
                            switch (index)
                            {
                                case 0:
                                    if (side[index] != EnumTile.NoTile)
                                    {
                                        GameObject goside = (GameObject)Resources.Load("Grass/Grass");
                                        Instantiate(goside, new Vector3(i,j+0.5f, 0.5f), Quaternion.identity);
                                        goside.transform.position = new Vector3(i, j + 0.5f, -0.5f);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}