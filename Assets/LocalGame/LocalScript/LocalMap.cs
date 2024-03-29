using System;
using System.Collections.Generic;
using UnityEngine;
using static World.GetType;

namespace World
{
    public class LocalMap : MonoBehaviour
    {
        public Tile[] _tiles;
        public Side[] _side;
        public MapDefinition _mapDefinition;
        private readonly TileSprite _tileSprite = new ();
        public int seed;

        public List<Transform[]> TreeTransforms;
        
        void Start()
        {
            _mapDefinition = new MapDefinition(seed);
            SetUpTile();
            AstarPath.active.Scan();
            TreeTransforms = new List<Transform[]>();
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
                                        Instantiate(s.visual, new Vector3(i, j + 1, 0), Quaternion.identity);
                                        break;
                                    // Right side
                                    case 1:
                                        Instantiate(s.visual, new Vector3(i + 1, j, 0), Quaternion.identity);
                                        break;
                                    // bottom side
                                    case 2:
                                        Instantiate(s.visual, new Vector3(i, j - 1, 0), Quaternion.identity);
                                        break;
                                    // left side
                                    default:
                                        Instantiate(s.visual, new Vector3(i - 1, j, 0), Quaternion.identity);
                                        break;
                                }
                            }
                        }
                        foreach (Corner corner in current.Corners)
                        {
                            if (corner != Corner.NoCorner)
                            {
                                switch (corner)
                                {
                                    case Corner.GrassCornerTopLeft:
                                        GameObject gcTL = Resources.Load<GameObject>(@"Grass\GrassCornerTopLeft");
                                        Instantiate(gcTL, new Vector3(i - 1, j + 1, 0), Quaternion.identity);
                                        break;
                                    case Corner.GrassCornerTopRight:
                                        GameObject gcTR = Resources.Load<GameObject>(@"Grass\GrassCornerTopRight");
                                        Instantiate(gcTR, new Vector3(i + 1, j + 1, 0), Quaternion.identity);
                                        break;
                                    case Corner.GrassCornerBotLeft:
                                        GameObject gcBL = Resources.Load<GameObject>(@"Grass\GrassCornerBotLeft");
                                        Instantiate(gcBL, new Vector3(i - 1, j - 1, 0), Quaternion.identity);
                                        break;
                                    case Corner.GrassCornerBotRight:
                                        GameObject gcBR = Resources.Load<GameObject>(@"Grass\GrassCornerBotRight");
                                        Instantiate(gcBR, new Vector3(i + 1, j - 1, 0), Quaternion.identity);
                                        break;
                                    case Corner.SnowCornerTopLeft:
                                        GameObject scTL = Resources.Load<GameObject>(@"Snow\SnowCornerTopLeft");
                                        Instantiate(scTL, new Vector3(i - 1, j + 1, 0), Quaternion.identity);
                                        break;
                                    case Corner.SnowCornerTopRight:
                                        GameObject scTR = Resources.Load<GameObject>(@"Snow\SnowCornerTopRight");
                                        Instantiate(scTR, new Vector3(i + 1, j + 1, 0), Quaternion.identity);
                                        break;
                                    case Corner.SnowCornerBotLeft:
                                        GameObject scBL = Resources.Load<GameObject>(@"Snow\SnowCornerBotLeft");
                                        Instantiate(scBL, new Vector3(i - 1, j - 1, 0), Quaternion.identity);
                                        break;
                                    case Corner.SnowCornerBotRight:
                                        GameObject scBR = Resources.Load<GameObject>(@"Snow\SnowCornerBotRight");
                                        Instantiate(scBR, new Vector3(i + 1, j - 1, 0), Quaternion.identity);
                                        break;
                                    case Corner.NoCorner:
                                    default:
                                        throw new ArgumentOutOfRangeException();
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
                                Instantiate(crabe, new Vector3(i + dec, j + dec, 0), Quaternion.identity);
                                break;
                            case Obj.Flower1:
                                GameObject flower1 = Resources.Load<GameObject>(@"Props\flower1");
                                Instantiate(flower1, new Vector3(i + dec, j + dec, 0), Quaternion.identity);
                                break;
                            case Obj.Flower2:
                                GameObject flower2 = Resources.Load<GameObject>(@"Props\flower2");
                                Instantiate(flower2, new Vector3(i + dec, j + dec, 0), Quaternion.identity);
                                break;
                            case Obj.Flower3:
                                GameObject flower3 = Resources.Load<GameObject>(@"Props\flower3");
                                Instantiate(flower3, new Vector3(i + dec, j + dec, 0), Quaternion.identity);
                                break;
                            case Obj.Flower4:
                                GameObject flower4 = Resources.Load<GameObject>(@"Props\flower4");
                                Instantiate(flower4, new Vector3(i + dec, j + dec, 0), Quaternion.identity);
                                break;
                            case Obj.GrassSnow1:
                                GameObject grassSnow1 = Resources.Load<GameObject>(@"Props\GrassSnow1");
                                Instantiate(grassSnow1, new Vector3(i + dec, j + dec, 0), Quaternion.identity);
                                break;
                            case Obj.GrassSnow2:
                                GameObject grassSnow2 = Resources.Load<GameObject>(@"Props\GrassSnow2");
                                Instantiate(grassSnow2, new Vector3(i + dec, j + dec, 0), Quaternion.identity);
                                break;
                            case Obj.GrassSnow3:
                                GameObject grassSnow3 = Resources.Load<GameObject>(@"Props\GrassSnow3");
                                Instantiate(grassSnow3, new Vector3(i + dec, j + dec, 0), Quaternion.identity);
                                break;
                            case Obj.GrassSnow4:
                                GameObject grassSnow4 = Resources.Load<GameObject>(@"Props\GrassSnow4");
                                Instantiate(grassSnow4, new Vector3(i + dec, j + dec, 0), Quaternion.identity);
                                break;
                            case Obj.StarFish1:
                                GameObject starFish1 = Resources.Load<GameObject>(@"Props\StarFish1");
                                Instantiate(starFish1, new Vector3(i + dec, j + dec, 0), Quaternion.identity);
                                break;
                            case Obj.StarFish2:
                                GameObject starFish2 = Resources.Load<GameObject>(@"Props\StarFish2");
                                Instantiate(starFish2, new Vector3(i + dec, j + dec, 0), Quaternion.identity);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    else if (current.HaveTree && CanPlaceTree(i, j, _mapDefinition))
                    {
                        if (IsGrass(current.TileType))
                        {
                            var treeGrass = Resources.Load<GameObject>(@"Tree\GrassTree");
                            Instantiate(treeGrass, new Vector3(i , j, -0.5f), Quaternion.identity);
                        }
                        else
                        {
                            var SnowTree = Resources.Load<GameObject>(@"Tree\SnowTree");
                            Instantiate(SnowTree, new Vector3(i , j, -0.5f), Quaternion.identity);
                        }
                    }
                    else if (current.HaveRock)
                    {
                        if (IsDirt(current.TileType))
                        {
                            GameObject rock1 = Resources.Load<GameObject>(@"Rock\Rock1");
                            Instantiate(rock1, new Vector3(i, j, -0.4f), Quaternion.identity);
                        }
                        else
                        {
                            GameObject rock2 = Resources.Load<GameObject>(@"Rock\Rock2");
                            Instantiate(rock2, new Vector3(i, j, -0.4f), Quaternion.identity);
                        }
                    }
                }
            }
        }
    }
}