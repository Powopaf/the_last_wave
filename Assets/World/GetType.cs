using System;

namespace World
{
    public static class GetType
    {
        public static bool IsDirt(EnumTile tile)
        {
            return tile is EnumTile.Dirt1 or EnumTile.Dirt2 or EnumTile.Dirt3 or EnumTile.Dirt4;
        }
        
        public static bool IsGrass(EnumTile tile)
        {
            return tile switch
            {
                EnumTile.DefaultGrass1 => true,
                EnumTile.DefaultGrass2 => true,
                EnumTile.DefaultGrass3 => true,
                EnumTile.DefaultGrass4 => true,
                EnumTile.Grass1 => true,
                EnumTile.Grass2 => true, 
                EnumTile.Grass3 => true,
                EnumTile.Grass4 => true,
                EnumTile.Grass5 => true,
                EnumTile.Grass6 => true,
                EnumTile.Grass7 => true,
                EnumTile.Grass8 => true,
                _ => false
            };
        }

        public static bool IsSnow(EnumTile tile)
        {
            return tile switch
            {
                EnumTile.Snow1 => true,
                EnumTile.Snow2 => true,
                EnumTile.Snow3 => true,
                EnumTile.SnowDefault1 => true,
                EnumTile.SnowDefault2 => true,
                EnumTile.SnowDefault3 => true,
                EnumTile.SnowDefault4 => true,
                _ => false
            };
        }

        
        public static bool IsSand(EnumTile tile)
        {
            return tile switch
            {
                EnumTile.Sand1 => true,
                EnumTile.Sand2 => true,
                EnumTile.Sand3 => true,
                EnumTile.Sand4 => true,
                EnumTile.Sand5 => true,
                EnumTile.Sand6 => true,
                EnumTile.SandDefault1 => true,
                EnumTile.SandDefault2 => true,
                EnumTile.SandDefault3 => true,
                EnumTile.SandDefault4 => true,
                _ => false
            };
        }

        public static bool IsWater(EnumTile tile)
        {
            return tile switch
            {
                EnumTile.Water1 => true,
                EnumTile.Water2 => true,
                EnumTile.Water3 => true,
                EnumTile.Water4 => true,
                EnumTile.Water5 => true,
                EnumTile.Water6 => true,
                _ => false
            };
        }

        private static bool IsInSide(int i, int j, int height, int width)
        {
            return i >= 0 && i < height && j >= 0 && j < width;
        }
        
        public static bool CanPlaceTree(int i, int j, MapDefinition mapDef)
        {
            TileDefinition[,] map = mapDef.Map;
            if (IsInSide(i, j + 1, mapDef.Height, mapDef.Width) && map[i,j + 1].HaveTree)
            {
                return false;
            }
            if (IsInSide(i, j + 2, mapDef.Height, mapDef.Width) && map[i,j + 2].HaveTree)
            {
                return false;
            }
            if (IsInSide(i + 1, j + 1, mapDef.Height, mapDef.Width) && map[i + 1,j + 1].HaveTree)
            {
                return false;
            }
            return !IsInSide(i + 1, j + 2, mapDef.Height, mapDef.Width) || !map[i + 1,j + 2].HaveTree;
        }

        public static double PlaceProps(Random rd)
        {
            return rd.Next(0, 2) == 0 ? rd.NextDouble() : rd.NextDouble() * -1;
        }
    }
}