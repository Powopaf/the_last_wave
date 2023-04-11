﻿using System;
using static World.PerlinNoise.PerlinNoise;
namespace World
{
    public class MapDefinition
    {
        public TileDefinition[,] Map { get; }
        public int Height => Map.GetLength(0);
        public int Width => Map.GetLength(1);

        public MapDefinition()
        {
            Map = new TileDefinition[50,80];
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
            GetNoiseTile();
        }

        private void GetNoiseTile()
        {
            float[,] noiseMap = GenerateNoiseMap(Width, Height, 64, new Random(0));
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    float noise = noiseMap[i, j];
                    Map[i, j] = new TileDefinition(GetTileNoise(noise));
                }
            }
        }

        private EnumTile GetTileNoise(float noise)
        {
            /*if (noise >= -1 && noise < -0.5 )
            {
                return EnumTile.Water1; // put deep water
            }*/
            if (noise >= -1 && noise < -0.6)
            {
                return EnumTile.Water1;
            }
            if (noise >= -0.6 && noise < -0.2)
            {
                return EnumTile.Sand1;
            }
            if (noise >= -0.2 && noise < 0.5)
            {
                return EnumTile.Grass1;
            }
            if (noise >= 0.5 && noise > 0.7)
            {
                return EnumTile.Dirt1;
            }
            return EnumTile.Snow1;
        }
        
        public bool IsGrass(EnumTile tile)
        {
            return tile switch
            {
                EnumTile.Grass1 => true,
                _ => false
            };
        }

        public bool IsSnow(EnumTile tile)
        {
            return tile switch
            {
                EnumTile.Snow1 => true,
                _ => false
            };
            {
                EnumTile.Snow1 => true
                _ => false
            };
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
    }
}
