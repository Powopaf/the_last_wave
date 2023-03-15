using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace World
{
    public class SeedGeneration : MapDefinition
    {
        private List<(EnumTile, double, int, int)> Seeds { get; } = new();
        private readonly Random _rd = new(0);
        private TileSprite _tileSprite;
        
        private double NextDouble()
        {
            double maxValue = 2;
            return _rd.NextDouble() * maxValue;
        }

        public void GenerateSeeds(int n)
        {
            List<(int, int)> postaken = new List<(int, int)>();
            for (int i = 0; i < n; i++)
            {
                int x = _rd.Next(0, Height-1);
                int y = _rd.Next(0, Width-1);
                if (!postaken.Contains((x,y)))
                {
                    postaken.Add((x,y));
                    EnumTile tile = _tileSprite.EnumTiles[_rd.Next(0, _tileSprite.Sprite.Count)];
                    double intensity = NextDouble();
                    Seeds.Add((tile, intensity, x, y));
                }
            }
        }

        public EnumTile Distance(int i, int j)
        {
            double dismin = Math.Sqrt((i - Seeds[0].Item3) * (i - Seeds[0].Item3)
                                      + (j - Seeds[0].Item4) * (j - Seeds[0].Item4));
            EnumTile seed = Seeds[0].Item1;
            foreach (var s in Seeds)
            {
                int x = s.Item3;
                int y = s.Item4;
                double dis = Math.Sqrt((i - x) * (i - x) + (j - y) * (j - y));
                if (dis < dismin)
                {
                    dismin = dis;
                    seed = s.Item1;
                }
            }
            return seed;
        }
    }
}