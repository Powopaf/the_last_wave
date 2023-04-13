using System;
using UnityEngine;
using static World.GetType;
using Random = System.Random;

namespace World
{
    public class Map : MonoBehaviour
    {
        private Tile[] _tiles;
        private Side[] _side;
        private MapDefinition _mapDefinition;
        private readonly TileSprite _tileSprite = new ();
        
        void Start()
        {
            _mapDefinition = new MapDefinition();
            //SetMapGen();
            SetUpTile();
        }

        private void SetMapGen() // don't work
        {
            var map = Resources.Load<GameObject>("MapGenerator");
            Instantiate(map, new Vector3(0, 0, 0), Quaternion.identity);
        }
        
        private void SetUpTile()
        {
            for (int i = 0; i < _mapDefinition.Height; i++)
            {
                for (int j = 0; j < _mapDefinition.Width; j++)
                {
                    TileDefinition current = _mapDefinition.Map[i, j];
                    var tile = _tiles[_tileSprite.Sprite[current.TileType]];
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
                                var s = _side[_tileSprite.Side[t]];
                                switch (index)
                                {
                                    // top Side
                                    case 0:
                                        Instantiate(s.visual, new Vector3(i, j + 1, -0.5f), Quaternion.identity);
                                        break;
                                    // Right side
                                    case 1:
                                        Instantiate(s.visual, new Vector3(i + 1, j, -0.5f), Quaternion.identity);
                                        break;
                                    // bottom side
                                    case 2:
                                        Instantiate(s.visual, new Vector3(i, j - 1, -0.5f), Quaternion.identity);
                                        break;
                                    // left side
                                    default:
                                        Instantiate(s.visual, new Vector3(i - 1, j, -0.5f), Quaternion.identity);
                                        break;
                                }
                            }
                        }
                    }
                    if (current.HaveProps)
                    {
                        float dec = (float)current.Prop.Item2;
                        switch (current.Prop.Item1)
                        {
                            case Obj.NoObj:
                                break;
                            case Obj.Crabe:
                                GameObject crabe = Resources.Load<GameObject>(@"Props\crabe");
                                Instantiate(crabe, new Vector3(i + dec, j + dec, -0.5f), Quaternion.identity);
                                break;
                            case Obj.Flower1:
                                GameObject flower1 = Resources.Load<GameObject>(@"Props\flower1");
                                Instantiate(flower1, new Vector3(i + dec, j + dec, -0.5f), Quaternion.identity);
                                break;
                            case Obj.Flower2:
                                GameObject flower2 = Resources.Load<GameObject>(@"Props\flower2");
                                Instantiate(flower2, new Vector3(i + dec, j + dec, -0.5f), Quaternion.identity);
                                break;
                            case Obj.Flower3:
                                GameObject flower3 = Resources.Load<GameObject>(@"Props\flower3");
                                Instantiate(flower3, new Vector3(i + dec, j + dec, -0.5f), Quaternion.identity);
                                break;
                            case Obj.Flower4:
                                GameObject flower4 = Resources.Load<GameObject>(@"Props\flower4");
                                Instantiate(flower4, new Vector3(i + dec, j + dec, -0.5f), Quaternion.identity);
                                break;
                            case Obj.GrassSnow1:
                                GameObject grassSnow1 = Resources.Load<GameObject>(@"Props\GrassSnow1");
                                Instantiate(grassSnow1, new Vector3(i + dec, j + dec, -0.5f), Quaternion.identity);
                                break;
                            case Obj.GrassSnow2:
                                GameObject grassSnow2 = Resources.Load<GameObject>(@"Props\GrassSnow2");
                                Instantiate(grassSnow2, new Vector3(i + dec, j + dec, -0.5f), Quaternion.identity);
                                break;
                            case Obj.GrassSnow3:
                                GameObject grassSnow3 = Resources.Load<GameObject>(@"Props\GrassSnow3");
                                Instantiate(grassSnow3, new Vector3(i + dec, j + dec, -0.5f), Quaternion.identity);
                                break;
                            case Obj.GrassSnow4:
                                GameObject grassSnow4 = Resources.Load<GameObject>(@"Props\GrassSnow4");
                                Instantiate(grassSnow4, new Vector3(i + dec, j + dec, -0.5f), Quaternion.identity);
                                break;
                            case Obj.StarFish1:
                                GameObject starFish1 = Resources.Load<GameObject>(@"Props\StarFish1");
                                Instantiate(starFish1, new Vector3(i + dec, j + dec, -0.5f), Quaternion.identity);
                                break;
                            case Obj.StarFish2:
                                GameObject starFish2 = Resources.Load<GameObject>(@"Props\StarFish2");
                                Instantiate(starFish2, new Vector3(i + dec, j + dec, -0.5f), Quaternion.identity);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
        }
    }
}