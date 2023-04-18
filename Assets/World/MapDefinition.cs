using System;
using static World.PerlinNoise.PerlinNoise;
using static World.GetType;

namespace World
{
    public class MapDefinition
    {
        public TileDefinition[,] Map { get; }
        public int Height => Map.GetLength(0);
        public int Width => Map.GetLength(1);

        private readonly int _seed;

        public MapDefinition(int s = 0)
        {
            _seed = s;
            Map = new TileDefinition[512,512]; // Do not put big number here or ...
            for (int i = 0; i < Width; i++)
            {
                Map[0, i] = new TileDefinition(EnumTile.WallBorderMap);
                Map[Height - 1, i] = new TileDefinition(EnumTile.WallBorderMap);
            }
            for (int j = 0; j < Height; j++)
            {
                Map[j, 0] = new TileDefinition(EnumTile.WallBorderMap);
                Map[j, Width - 1] = new TileDefinition(EnumTile.WallBorderMap);
            }
            GetNoiseTile(_seed); // can put seed here
            PrettyMap(_seed);
            SetSideTile();
        }

        private bool IsInSide(int i, int j)
        {
            return i >= 0 && i < Height && j >= 0 && j < Width;
        }

        private void GetNoiseTile(int seed = 0)
        {
            float[,] noiseMap = GenerateNoiseMap(Width, Height, 64, new Random(seed));
            for (int i = 1; i < Height - 1; i++)
            {
                for (int j = 1; j < Width - 1; j++)
                {
                    float noise = noiseMap[i, j];
                    Map[i, j] = new TileDefinition(GetTileNoise(noise));
                }
            }
        }

        
        private void DefaultMap()
        {
            System.Random rd = new System.Random(0);
            for (int i = 1; i < Height - 1; i++)
            {
                for (int j = 1; j < Width - 1; j++)
                {
                    int a = rd.Next(1, 3);
                    var tile = a == 1 ? EnumTile.Dirt1 : EnumTile.Grass1;
                    Map[i, j] = new TileDefinition(tile);
                }
            }
        }

        
        private void SeedMap(int n = 1)
        {
            SeedGeneration seed = new SeedGeneration(Height, Width);
            seed.GenerateSeeds(n);
            for (int i = 1; i < Height - 1; i++)
            {
                for (int j = 1; j < Width - 1; j++)
                {
                    Map[i, j] = new TileDefinition(seed.Distance(i, j));
                }
            }
        }

        private void PrettyMap(int seed = 0)
        {
            Random rd = new Random(seed);
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int agree = 0;
                    if (Map[i, j].TileType == EnumTile.Dirt1)
                    {
                        if (IsInSide(i + 1, j) && Map[ i + 1, j].TileType == EnumTile.Dirt1)
                        {
                            agree++;
                        }
                        if (IsInSide(i, j - 1) && Map[i,j - 1].TileType == EnumTile.Dirt1)
                        {
                            agree++;
                        }
                        if (IsInSide(i + 1,j - 1) && Map[i + 1, j - 1].TileType == EnumTile.Dirt1)
                        {
                            agree++;
                        }
                        if (agree == 3)
                        {
                            Map[i + 1, j].TileType = EnumTile.Dirt2;
                            Map[i, j - 1].TileType = EnumTile.Dirt3;
                            Map[i + 1, j - 1].TileType = EnumTile.Dirt4;
                        }
                        if (rd.Next(0,9) == 0)
                        {
                            SpawnRock(i, j);
                        }
                    }
                    
                    else if (Map[i,j].TileType == EnumTile.Sand1)
                    {
                        if (IsInSide(i + 1, j)&&Map[i + 1, j].TileType == EnumTile.Sand1)
                        {
                            agree++;
                        }
                        if (IsInSide(i, j - 1) && Map[i, j - 1].TileType == EnumTile.Sand1)
                        {
                            agree++;
                        }
                        if (IsInSide(i + 1, j - 1) && Map[i + 1, j - 1].TileType == EnumTile.Sand1)
                        {
                            agree++;
                        }
                        if (agree == 3)
                        {
                            Map[i, j].TileType = EnumTile.SandDefault1;
                            Map[i + 1, j].TileType = EnumTile.SandDefault2;
                            Map[i, j - 1].TileType = EnumTile.SandDefault3;
                            Map[i + 1, j - 1].TileType = EnumTile.SandDefault4;
                        }
                        Map[i, j].HaveProps = rd.Next(0, 10) == 0;
                        if (Map[i, j].HaveProps)
                        {
                            Map[i, j].Prop = (Obj.Crabe, PlaceProps(rd));
                        }
                        if (rd.Next(0,9) == 0)
                        {
                            SpawnRock(i, j);
                        }
                    }
                    
                    else if (Map[i,j].TileType == EnumTile.Snow1)
                    {
                        if (IsInSide(i + 1, j)&&Map[i + 1, j].TileType == EnumTile.Snow1)
                        {
                            agree++;
                        }
                        if (IsInSide(i, j - 1) && Map[i, j - 1].TileType == EnumTile.Snow1)
                        {
                            agree++;
                        }
                        if (IsInSide(i + 1, j - 1) && Map[i + 1, j - 1].TileType == EnumTile.Snow1)
                        {
                            agree++;
                        }
                        if (agree == 3)
                        {
                            Map[i, j].TileType = EnumTile.SnowDefault1;
                            Map[i + 1, j].TileType = EnumTile.SnowDefault2;
                            Map[i, j - 1].TileType = EnumTile.SnowDefault3;
                            Map[i + 1, j - 1].TileType = EnumTile.SnowDefault4;
                        }
                        Map[i, j].HaveProps = rd.Next(0, 5) == 0;
                        if (Map[i,j].HaveProps)
                        {
                            Map[i, j].Prop = rd.Next(1, 5) switch
                            {
                                1 => (Obj.GrassSnow1, PlaceProps(rd)),
                                2 => (Obj.GrassSnow2, PlaceProps(rd)),
                                3 => (Obj.GrassSnow3, PlaceProps(rd)),
                                4 => (Obj.GrassSnow4, PlaceProps(rd)),
                                _ => (Obj.NoObj, 0)
                            };
                        }
                        if (rd.Next(0,10) == 0)
                        {
                            SpawnTree(i,j);
                        }
                        if (rd.Next(0,9) == 0)
                        {
                            SpawnRock(i, j);
                        }
                    }
                    
                    else if (Map[i,j].TileType == EnumTile.Water1 && rd.Next(0, 5) == 0)
                    {
                        if (IsInSide(i + 1, j) && Map[i + 1, j].TileType == EnumTile.Water1)
                        {
                            agree++;
                        }
                        if (IsInSide(i + 2, j) && Map[i + 2, j].TileType == EnumTile.Water1)
                        {
                            agree++;
                        }
                        if (IsInSide(i, j - 1) && Map[i, j - 1].TileType == EnumTile.Water1)
                        {
                            agree++;
                        }
                        if (IsInSide(i + 1, j - 1) && Map[i + 1, j - 1].TileType == EnumTile.Water1)
                        {
                            agree++;
                        }
                        if (IsInSide(i + 2, j - 1) && Map[i + 2, j - 1].TileType == EnumTile.Water1)
                        {
                            agree++;
                        }
                        if (agree == 5)
                        {
                            Map[i + 1, j].TileType = EnumTile.Water2;
                            Map[i + 2, j].TileType = EnumTile.Water3;
                            Map[i, j - 1].TileType = EnumTile.Water4;
                            Map[i + 1, j - 1].TileType = EnumTile.Water5;
                            Map[i + 2, j - 1].TileType = EnumTile.Water6;
                        }
                        Map[i, j].HaveProps = rd.Next(0, 5) == 0;
                        if (Map[i,j].HaveProps)
                        {
                            Map[i, j].Prop = rd.Next(1, 3) == 1
                                ? (Obj.StarFish1, PlaceProps(rd))
                                : (Obj.StarFish2, PlaceProps(rd));
                        }
                    }
                    
                    else if (Map[i,j].TileType == EnumTile.DefaultGrass1)
                    {
                        if (IsInSide(i + 1, j)&&Map[i + 1, j].TileType == EnumTile.DefaultGrass1)
                        {
                            agree++;
                        }
                        if (IsInSide(i, j - 1) && Map[i, j - 1].TileType == EnumTile.DefaultGrass1)
                        {
                            agree++;
                        }
                        if (IsInSide(i + 1, j - 1) && Map[i + 1, j - 1].TileType == EnumTile.DefaultGrass1)
                        {
                            agree++;
                        }
                        if (agree == 3)
                        {
                            Map[i + 1, j].TileType = EnumTile.DefaultGrass2;
                            Map[i, j - 1].TileType = EnumTile.DefaultGrass3;
                            Map[i + 1, j - 1].TileType = EnumTile.DefaultGrass4;
                        }
                        Map[i, j].HaveProps = rd.Next(0, 5) == 0;
                        if (Map[i,j].HaveProps)
                        {
                            Map[i, j].Prop = rd.Next(1, 5) switch
                            {
                                1 => (Obj.Flower1, PlaceProps(rd)),
                                2 => (Obj.Flower2, PlaceProps(rd)),
                                3 => (Obj.Flower3, PlaceProps(rd)),
                                4 => (Obj.Flower4, PlaceProps(rd)),
                                _ => (Obj.NoObj, 0)
                            };
                        }
                        if (rd.Next(0,10) == 0)
                        {
                            SpawnTree(i,j);
                        }
                        if (rd.Next(0,9) == 0)
                        {
                            SpawnRock(i, j);
                        }
                    }
                }
            }
        }


        private void SetSideTile()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    TileDefinition current = Map[i, j];
                    if (IsGrass(current.TileType))
                    {
                        bool haveSide = false;
                        RoundGrass(i, j, ref haveSide);
                        Map[i, j].HaveSide = haveSide;
                    }
                    else if (IsWater(current.TileType))
                    {
                        bool haveSide = false;
                        RoundWater(i, j, ref haveSide);
                        Map[i, j].HaveSide = haveSide;
                        
                    }
                    else if (IsSnow(current.TileType))
                    {
                        bool haveSide = false;
                        RoundSnow(i, j , ref haveSide);
                        Map[i, j].HaveSide = haveSide;
                    }
                }
            }
        }

        private void RoundGrass(int i, int j, ref bool haveSide)
        {
            int cornerTL = 0;
            int cornerTR = 0;
            int cornerBL = 0;
            int cornerBR = 0;
            TileDefinition current = Map[i, j];
            if (IsInSide(i, j + 1) && !IsGrass(Map[i, j + 1].TileType))
            {
                haveSide = true;
                cornerTL++;
                cornerTR++;
                current.Side[0] = EnumTile.GrassSideTop;
            }
            if (IsInSide(i + 1, j) && !IsGrass(Map[i + 1, j].TileType))
            {
                haveSide = true;
                cornerTR++;
                cornerBR++;
                current.Side[1] = EnumTile.GrassSideRight;
            }
            if (IsInSide(i, j - 1) && !IsGrass(Map[i, j - 1].TileType))
            {
                haveSide = true;
                cornerBL++;
                cornerBR++;
                current.Side[2] = EnumTile.GrassSideBot;
            }
            if (IsInSide(i - 1, j) && !IsGrass(Map[i - 1, j].TileType))
            {
                haveSide = true;
                cornerTL++;
                cornerBL++;
                current.Side[3] = EnumTile.GrassSideLeft;
            }
            current.Corners[0] = cornerTL == 2 ? Corner.GrassCornerTopLeft : Corner.NoCorner;
            current.Corners[1] = cornerTR == 2 ? Corner.GrassCornerTopRight : Corner.NoCorner;
            current.Corners[2] = cornerBL == 2 ? Corner.GrassCornerBotLeft : Corner.NoCorner;
            current.Corners[3] = cornerBR == 2 ? Corner.GrassCornerBotRight : Corner.NoCorner;
        }
        
        private void RoundSnow(int i, int j, ref bool haveSide)
        {
            int cornerTL = 0;
            int cornerTR = 0;
            int cornerBL = 0;
            int cornerBR = 0;
            TileDefinition current = Map[i, j];
            if (IsInSide(i, j + 1) && !IsSnow(Map[i, j + 1].TileType))
            {
                haveSide = true;
                cornerTL++;
                cornerTR++;
                current.Side[0] = EnumTile.SnowSideTop1;
            }
            if (IsInSide(i + 1, j) && !IsSnow(Map[i + 1, j].TileType))
            {
                haveSide = true;
                cornerTR++;
                cornerBR++;
                current.Side[1] = EnumTile.SnowSideRight;
            }
            if (IsInSide(i, j - 1) && !IsSnow(Map[i, j - 1].TileType))
            {
                haveSide = true;
                cornerBL++;
                cornerBR++;
                current.Side[2] = EnumTile.SnowSideBot1;
            }
            if (IsInSide(i - 1, j) && !IsSnow(Map[i - 1, j].TileType))
            {
                haveSide = true;
                cornerTL++;
                cornerBL++;
                current.Side[3] = EnumTile.SnowSideLeft;
            }
            current.Corners[0] = cornerTL == 2 ? Corner.SnowCornerTopLeft : Corner.NoCorner;
            current.Corners[1] = cornerTR == 2 ? Corner.SnowCornerTopRight : Corner.NoCorner;
            current.Corners[2] = cornerBL == 2 ? Corner.SnowCornerBotLeft : Corner.NoCorner;
            current.Corners[3] = cornerBR == 2 ? Corner.SnowCornerBotRight : Corner.NoCorner;
        }
        
        private void RoundWater(int i, int j, ref bool haveSide)
        {
            TileDefinition current = Map[i, j];
            if (IsInSide(i, j + 1) && !IsWater(Map[i, j + 1].TileType))
            {
                haveSide = true;
                current.Side[0] = EnumTile.WaterSideTop1;
            }
            if (IsInSide(i + 1, j) && !IsWater(Map[i + 1, j].TileType))
            {
                haveSide = true;
                current.Side[1] = EnumTile.WaterSideRight;
            }
            if (IsInSide(i, j - 1) && !IsWater(Map[i, j - 1].TileType))
            {
                haveSide = true;
                current.Side[2] = EnumTile.WaterSideBot1;
            }
            if (IsInSide(i - 1, j) && !IsWater(Map[i - 1, j].TileType))
            {
                haveSide = true;
                current.Side[3] = EnumTile.WaterSideLeft;
            }
        }

        private void SpawnTree(int i, int j)
        {

            Map[i, j].HaveTree = !Map[i, j].HaveProps;
        }

        private void SpawnRock(int i, int j)
        {
            Map[i, j].HaveRock = !Map[i, j].HaveTree && !Map[i, j].HaveProps;
        }
    }
}
