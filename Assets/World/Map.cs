using System;
using UnityEngine;
using static World.GetType;
using System.Linq;
using GameObject = UnityEngine.GameObject;

namespace World
{
    public class Map : MonoBehaviour
    {
        public Tile[] _tiles;
        public Side[] _side;
        private MapDefinition _mapDefinition;
        private readonly TileSprite _tileSprite = new ();
        public int seed;
        
        void Start()
        {
            _mapDefinition = new MapDefinition(seed);
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
                                        Instantiate(s.visual, new Vector3(i, j + 1, -0.1f), Quaternion.identity);
                                        break;
                                    // Right side
                                    case 1:
                                        Instantiate(s.visual, new Vector3(i + 1, j, -0.1f), Quaternion.identity);
                                        break;
                                    // bottom side
                                    case 2:
                                        Instantiate(s.visual, new Vector3(i, j - 1, -0.1f), Quaternion.identity);
                                        break;
                                    // left side
                                    default:
                                        Instantiate(s.visual, new Vector3(i - 1, j, -0.1f), Quaternion.identity);
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
                                        Instantiate(gcTL, new Vector3(i - 1, j + 1, -0.1f), Quaternion.identity);
                                        break;
                                    case Corner.GrassCornerTopRight:
                                        GameObject gcTR = Resources.Load<GameObject>(@"Grass\GrassCornerTopRight");
                                        Instantiate(gcTR, new Vector3(i + 1, j + 1, -0.1f), Quaternion.identity);
                                        break;
                                    case Corner.GrassCornerBotLeft:
                                        GameObject gcBL = Resources.Load<GameObject>(@"Grass\GrassCornerBotLeft");
                                        Instantiate(gcBL, new Vector3(i - 1, j - 1, -0.1f), Quaternion.identity);
                                        break;
                                    case Corner.GrassCornerBotRight:
                                        GameObject gcBR = Resources.Load<GameObject>(@"Grass\GrassCornerBotRight");
                                        Instantiate(gcBR, new Vector3(i + 1, j - 1, -0.1f), Quaternion.identity);
                                        break;
                                    case Corner.SnowCornerTopLeft:
                                        GameObject scTL = Resources.Load<GameObject>(@"Snow\SnowCornerTopLeft");
                                        Instantiate(scTL, new Vector3(i - 1, j + 1, -0.1f), Quaternion.identity);
                                        break;
                                    case Corner.SnowCornerTopRight:
                                        GameObject scTR = Resources.Load<GameObject>(@"Snow\SnowCornerTopRight");
                                        Instantiate(scTR, new Vector3(i + 1, j + 1, -0.1f), Quaternion.identity);
                                        break;
                                    case Corner.SnowCornerBotLeft:
                                        GameObject scBL = Resources.Load<GameObject>(@"Snow\SnowCornerBotLeft");
                                        Instantiate(scBL, new Vector3(i - 1, j - 1, -0.1f), Quaternion.identity);
                                        break;
                                    case Corner.SnowCornerBotRight:
                                        GameObject scBR = Resources.Load<GameObject>(@"Snow\SnowCornerBotRight");
                                        Instantiate(scBR, new Vector3(i + 1, j - 1, -0.1f), Quaternion.identity);
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
                                Instantiate(crabe, new Vector3(i + dec, j + dec, -0.3f), Quaternion.identity);
                                break;
                            case Obj.Flower1:
                                GameObject flower1 = Resources.Load<GameObject>(@"Props\flower1");
                                Instantiate(flower1, new Vector3(i + dec, j + dec, -0.3f), Quaternion.identity);
                                break;
                            case Obj.Flower2:
                                GameObject flower2 = Resources.Load<GameObject>(@"Props\flower2");
                                Instantiate(flower2, new Vector3(i + dec, j + dec, -0.3f), Quaternion.identity);
                                break;
                            case Obj.Flower3:
                                GameObject flower3 = Resources.Load<GameObject>(@"Props\flower3");
                                Instantiate(flower3, new Vector3(i + dec, j + dec, -0.3f), Quaternion.identity);
                                break;
                            case Obj.Flower4:
                                GameObject flower4 = Resources.Load<GameObject>(@"Props\flower4");
                                Instantiate(flower4, new Vector3(i + dec, j + dec, -0.3f), Quaternion.identity);
                                break;
                            case Obj.GrassSnow1:
                                GameObject grassSnow1 = Resources.Load<GameObject>(@"Props\GrassSnow1");
                                Instantiate(grassSnow1, new Vector3(i + dec, j + dec, -0.3f), Quaternion.identity);
                                break;
                            case Obj.GrassSnow2:
                                GameObject grassSnow2 = Resources.Load<GameObject>(@"Props\GrassSnow2");
                                Instantiate(grassSnow2, new Vector3(i + dec, j + dec, -0.3f), Quaternion.identity);
                                break;
                            case Obj.GrassSnow3:
                                GameObject grassSnow3 = Resources.Load<GameObject>(@"Props\GrassSnow3");
                                Instantiate(grassSnow3, new Vector3(i + dec, j + dec, -0.3f), Quaternion.identity);
                                break;
                            case Obj.GrassSnow4:
                                GameObject grassSnow4 = Resources.Load<GameObject>(@"Props\GrassSnow4");
                                Instantiate(grassSnow4, new Vector3(i + dec, j + dec, -0.3f), Quaternion.identity);
                                break;
                            case Obj.StarFish1:
                                GameObject starFish1 = Resources.Load<GameObject>(@"Props\StarFish1");
                                Instantiate(starFish1, new Vector3(i + dec, j + dec, -0.3f), Quaternion.identity);
                                break;
                            case Obj.StarFish2:
                                GameObject starFish2 = Resources.Load<GameObject>(@"Props\StarFish2");
                                Instantiate(starFish2, new Vector3(i + dec, j + dec, -0.3f), Quaternion.identity);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    else if (current.HaveTree && CanPlaceTree(i, j, _mapDefinition))
                    {
                        if (IsGrass(current.TileType))
                        {
                            var botLeftLeaf = Resources.Load<GameObject>(@"Tree\GrassTree\BotLeftLeaf");
                            var botRightLeaf = Resources.Load<GameObject>(@"Tree\GrassTree\BotRightLeaf");
                            var topLeftLeaf = Resources.Load<GameObject>(@"Tree\GrassTree\TopLeftLeaf");
                            var topRightLeaf = Resources.Load<GameObject>(@"Tree\GrassTree\TopRightLeaf");
                            var trunkLeft = Resources.Load<GameObject>(@"Tree\GrassTree\TrunkLeft");
                            var trunkRight = Resources.Load<GameObject>(@"Tree\GrassTree\TrunkRight");
                            Instantiate(trunkLeft, new Vector3(i - 0.5f, j, -0.5f), Quaternion.identity);
                            Instantiate(trunkRight, new Vector3(i + 0.5f, j, -0.5f), Quaternion.identity);
                            Instantiate(botLeftLeaf, new Vector3(i - 0.5f, j + 1, -0.5f), Quaternion.identity);
                            Instantiate(botRightLeaf, new Vector3(i + 0.5f, j + 1, -0.5f), Quaternion.identity);
                            Instantiate(topLeftLeaf, new Vector3(i - 0.5f, j + 2, -0.5f), Quaternion.identity);
                            Instantiate(topRightLeaf, new Vector3(i + 0.5f, j + 2, -0.5f), Quaternion.identity);
                        }
                        else
                        {
                            var botLeftLeaf = Resources.Load<GameObject>(@"Tree\SnowTree\BotLeftLeaf");
                            var botMidLeaf = Resources.Load<GameObject>(@"Tree\SnowTree\BotMidLeaf");
                            var botRightLeaf = Resources.Load<GameObject>(@"Tree\SnowTree\BotRightLeaf");
                            var topLeftLeaf = Resources.Load<GameObject>(@"Tree\SnowTree\TopLeftLeaf");
                            var topMidLeaf = Resources.Load<GameObject>(@"Tree\SnowTree\TopMidLeaf");
                            var topRightLeaf = Resources.Load<GameObject>(@"Tree\SnowTree\TopRightLeaf");
                            var trunkLeft = Resources.Load<GameObject>(@"Tree\SnowTree\TrunkLeft");
                            var trunkMid = Resources.Load<GameObject>(@"Tree\SnowTree\TrunkMid");
                            var trunkRight = Resources.Load<GameObject>(@"Tree\SnowTree\TrunkRight");
                            Instantiate(trunkLeft, new Vector3(i - 1, j, -0.5f), Quaternion.identity);
                            Instantiate(trunkMid, new Vector3(i, j, -0.5f), Quaternion.identity);
                            Instantiate(trunkRight, new Vector3(i + 1, j, -0.5f), Quaternion.identity);
                            Instantiate(botLeftLeaf, new Vector3(i - 1, j + 1, -0.5f), Quaternion.identity);
                            Instantiate(botMidLeaf, new Vector3(i, j + 1, -0.5f), Quaternion.identity);
                            Instantiate(botRightLeaf, new Vector3(i + 1, j + 1, -0.5f), Quaternion.identity);
                            Instantiate(topLeftLeaf, new Vector3(i - 1, j + 2, -0.5f), Quaternion.identity);
                            Instantiate(topMidLeaf, new Vector3(i, j + 2, -0.5f), Quaternion.identity);
                            Instantiate(topRightLeaf, new Vector3(i + 1, j + 2, -0.5f), Quaternion.identity);
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