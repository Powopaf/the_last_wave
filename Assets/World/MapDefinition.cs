﻿using System;
using static World.PerlinNoise.PerlinNoise;
using static World.GetType;

namespace World
{
    public class MapDefinition
    {
        public TileDefinition[,] Map { get; }
        public int Height => Map.GetLength(0);
        public int Width => Map.GetLength(1);

        public MapDefinition()
        {
            Map = new TileDefinition[512,512];
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
            GetNoiseTile(); // can put seed here
            PrettyMap();
        }

        private bool IsInSide(int i, int j)
        {
            return i >= 0 && i < Height && j >= 0 && j < Width;
        }

        private void GetNoiseTile(int seed = 0)
        {
            float[,] noiseMap = GenerateNoiseMap(Width, Height, 64, new Random(seed));
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
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

        private void RoundedGrass(int i, int j)
        {
            TileDefinition cur = Map[i, j]; 
            if (Map[i,j + 1].TileType != EnumTile.WallBorderMap && Map[i,j + 1].TileType != cur.TileType) 
            { 
                Map[i, j + 1].HasSide = true; 
                Map[i, j + 1].Side[0] = EnumTile.GrassSideTop;
            }
            if (Map[i, j - 1].TileType != EnumTile.WallBorderMap && Map[i, j - 1].TileType != cur.TileType)
            {
                Map[i, j + 1].HasSide = true;
                Map[i, j + 1].Side[1] = EnumTile.GrassSideBot;
            }
            if (Map[i + 1, j].TileType != EnumTile.WallBorderMap && Map[i + 1 ,j].TileType != cur.TileType)
            {
                Map[i, j + 1].HasSide = true;
                Map[i, j + 1].Side[2] = EnumTile.GrassSideRight;
            }
            if (Map[i - 1, j].TileType != EnumTile.WallBorderMap && Map[i - 1, j].TileType != cur.TileType)
            {
                Map[i, j + 1].HasSide = true;
                Map[i, j + 1].Side[3] = EnumTile.GrassSideLeft;
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
                    }
                    
                    else if (Map[i,j].TileType == EnumTile.Water1 && rd.Next(0, 10) == 0)
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
                    }
                }
            }
        }
    }
}
