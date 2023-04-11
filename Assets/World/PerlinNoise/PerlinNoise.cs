using System;

namespace World.PerlinNoise
{
    public static class PerlinNoise
    {
        private static float Noise(int x, int y)
        {
            int seed1 = x + y * 42;
            int seed2 = (seed1 << 13) ^ seed1; // ^ = XOR
            int seed3 = seed2 * seed2 * seed2 * 42424242;
            float seed4 = seed3 & Int32.MaxValue;
            float max = Int32.MaxValue;
            float seed5 = seed4 / max * 2;
            float lastSeed = 1 - seed5;
            return lastSeed;
        }

        private static float SmoothNoise(int x, int y)
        {
            float sumCorner = (Noise(x - 1, y - 1) + Noise(x + 1, y + 1) 
                                                   + Noise(x + 1, y - 1) + Noise(x - 1, y + 1))/16;
            float sumCross = (Noise(x + 1, y) + Noise(x - 1, y)
                                              + Noise(x, y + 1) + Noise(x, y - 1)) / 8;
            float sumUs = Noise(x, y)/4;
            return sumCorner + sumCross + sumUs;
        }

        private static float Lerp(float a, float b, float t)
        {
            return (b - a) * t + a;
        }

        private static float InterpolatedNoise(float x, float y)
        {
            float smoothUs = SmoothNoise((int)x, (int)y);
            float smoothRight = SmoothNoise((int)(x + 1), (int)y);
            float smoothTop = SmoothNoise((int)x, (int)(y + 1));
            float smoothTopR = SmoothNoise((int)(x + 1), (int)(y + 1));
            float lerp1 = Lerp(smoothUs, smoothRight, x - (int)x);
            float lerp2 = Lerp(smoothTop, smoothTopR, x - (int)x);
            return Lerp(lerp1, lerp2, y - (int)y);
        }

        private static double GetPerlinValue(float x, float y, float persistence, int octaves)
        {
            float interpolate = 0;
            for (int i = 0; i <= octaves; i++)
            {
                var frequency = (float)Math.Pow(2, i);
                var amplitude = (float)Math.Pow(persistence, i);
                interpolate += InterpolatedNoise(x * frequency, y * frequency) * amplitude;
            }
            return interpolate;
        }
        
        public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float noiseScale, Random random)
        {
            if (mapHeight <= 0 || mapWidth <=0)
            {
                throw new ArgumentException("Height and width must be greater than 0");
            }
            if (noiseScale <= 0)
            {
                throw new ArgumentException("Noise scale must be greater than 0");
            }
            int x = random.Next(0, 100000);
            int y = random.Next(0, 100000);
            float[,] map = new float[mapHeight, mapWidth];
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    map[i, j] = (float)GetPerlinValue(i / noiseScale + x, j / noiseScale + y, 0.5f, 12);
                }
            }
            return map;
        }
        
        public static EnumTile GetTileNoise(float noise)
        {
            if (noise >= -1 && noise < -0.15)
            {
                return EnumTile.Water1;
            }
            if (noise >= -0.15 && noise < -0.05)
            {
                return EnumTile.Sand1;
            }
            if (noise >= -0.05 && noise < 0.3)
            {
                return EnumTile.Grass1;
            }
            if (noise >= 0.3 && noise < 0.5)
            {
                return EnumTile.Dirt1;
            }
            return EnumTile.Snow1;
        }
    }
}