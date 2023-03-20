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
                        if (_mapDefinition.IsGrass(_mapDefinition.Map[i,j].TileType))
                        {
                            SetUpSideTileGrass(i,j);
                        }
                    }
                }
            }
        }

        void SetUpSideTileGrass(int i, int j)
        {
            EnumTile[] side = _mapDefinition.Map[i, j].Side;
            for (var index = 0; index < side.Length; index++)
            {
                switch (index)
                {
                    case 0:
                        if (side[index] != EnumTile.NoTile)
                        {
                            GameObject goside = (GameObject)Resources.Load("Grass/GrassSideTop1");
                            Instantiate(goside, new Vector3(i, j, -0.5f), Quaternion.identity);
                        }
                        break;
                    case 1:
                        if (side[index] != EnumTile.NoTile)
                        {
                            GameObject goside = (GameObject)Resources.Load("Grass/GrassSideBot1");
                            Instantiate(goside, new Vector3(i, j, -0.5f), Quaternion.identity);
                        }
                        break;
                    case 2:
                        if (side[index] != EnumTile.NoTile)
                        {
                            GameObject goside = (GameObject)Resources.Load("Grass/GrassSideRight");
                            Instantiate(goside, new Vector3(i+1, j, -0.5f), Quaternion.identity);
                        }
                        break;
                    default:
                        if (side[index] != EnumTile.NoTile)
                        {
                            GameObject goside = (GameObject)Resources.Load("Grass/GrassSideLeft");
                            Instantiate(goside, new Vector3(i, j, -0.5f), Quaternion.identity);
                        }
                        break;
                }
            }
        }
    }
}