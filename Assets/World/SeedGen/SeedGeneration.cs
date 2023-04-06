using System;
using System.Collections.Generic;

namespace World
{
    public class SeedGeneration
    {
        private List<(EnumTile, double, int, int)> Seeds { get; } = new(); 
        private readonly Random _rd = new(1);
        private readonly TileSprite _tileSprite = new ();
        private readonly int _height;
        private readonly int _width;

        public SeedGeneration(int height, int width)
        {
            _height = height;
            _width = width;
        }
        public void GenerateSeeds(int n)
        {
            List<(int, int)> postaken = new List<(int, int)>();
            for (int i = 0; i < n; i++)
            {
                int x = _rd.Next(1, _height-2);
                int y = _rd.Next(1, _width-2);
                if (!postaken.Contains((x,y)))
                {
                    postaken.Add((x,y));
                    EnumTile tile = _tileSprite.Tiles[_rd.Next(1, _tileSprite.Sprite.Count)];
                    double intensity = _rd.NextDouble() + _rd.Next(0, 2);
                    if (intensity == 0)
                    {
                        intensity = 0.1;
                    }
                    Seeds.Add((tile, intensity, x, y));
                }
            }
        }

        public EnumTile Distance(int i, int j)
        {
            double distmin = Math.Sqrt((i - Seeds[0].Item3) * (i - Seeds[0].Item3)
                                     + (j - Seeds[0].Item4) * (j - Seeds[0].Item4));
            EnumTile seed = Seeds[0].Item1;
            foreach (var s in Seeds)
            {
                int x = s.Item3;
                int y = s.Item4;
                double dis = Math.Sqrt((i - x) * (i - x) + (j - y) * (j - y)) * s.Item2;
                if (dis < distmin)
                {
                    distmin = dis;
                    seed = s.Item1;
                }
            }
            return seed;
        }
    }
}