using System.Collections.Generic;

namespace World
{
    public class TileSprite
    {
        public IDictionary<EnumTile, int> Sprite { get; }
        public IDictionary<int, EnumTile> Tiles { get; }

        public TileSprite()
        {
            Sprite = new Dictionary<EnumTile, int>
            {
                { EnumTile.WallBorderMap, 0 },
                { EnumTile.Dirt1, 1 },
                { EnumTile.Dirt2, 2 },
                { EnumTile.Dirt3, 3 },
                { EnumTile.Dirt4, 4 },
                { EnumTile.Sand1, 5 },
                { EnumTile.Sand2, 6 },
                { EnumTile.Sand3, 7 },
                { EnumTile.Sand4, 8 },
                { EnumTile.Sand5, 9 },
                { EnumTile.Sand6, 10 },
                { EnumTile.SandDefault1, 11 },
                { EnumTile.SandDefault2, 12 },
                { EnumTile.SandDefault3, 13 },
                { EnumTile.SandDefault4, 14 },
                { EnumTile.Snow1, 15 },
                { EnumTile.Snow2, 16 },
                { EnumTile.Snow3, 17 },
                { EnumTile.SnowDefault1, 18 },
                { EnumTile.SnowDefault2, 19 },
                { EnumTile.SnowDefault3, 20 },
                { EnumTile.SnowDefault4, 21 },
                { EnumTile.Water1, 22 },
                { EnumTile.Water2, 23 },
                { EnumTile.Water3, 24 },
                { EnumTile.Water4, 25 },
                { EnumTile.Water5, 26 },
                { EnumTile.Water6, 27 },
                { EnumTile.DefaultGrass1, 28 },
                { EnumTile.DefaultGrass2, 29 },
                { EnumTile.DefaultGrass3, 30 },
                { EnumTile.DefaultGrass4, 31 },
                { EnumTile.Grass1, 32 },
                { EnumTile.Grass2, 33 },
                { EnumTile.Grass3, 34 },
                { EnumTile.Grass4, 35 },
                { EnumTile.Grass5, 36 },
                { EnumTile.Grass6, 37 },
                { EnumTile.Grass7, 38 },
                { EnumTile.Grass8, 39 }
            };

            Tiles = new Dictionary<int, EnumTile>();
            foreach (KeyValuePair<EnumTile, int> kv in Sprite)
            {
                Tiles[kv.Value] = (EnumTile)Sprite[kv.Key];
            }
        }
    }
}