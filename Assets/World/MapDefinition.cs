namespace World
{
    public class MapDefinition
    {
        public TileDefinition[,] Map { get; }
        public int Height => Map.GetLength(0);
        public int Width => Map.GetLength(1);

        public MapDefinition()
        {
            Map = new TileDefinition[10, 15];
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
            SeedMap(5);
            for (int i = 0; i < Height - 1; i++)
            {
                for (int j = 0; j < Width - 1; j++)
                {
                    if (IsGrass(Map[i,j].TileType))
                    {
                        RoundedGrass(i,j);
                    }

                    if (IsDirt(Map[i,j].TileType))
                    {
                        
                    }

                    if (IsSand(Map[i,j].TileType))
                    {
                        
                    }
                    if (IsSnow(Map[i,j].TileType))
                    {
                        
                    }
                }
            }
        }

        private bool IsGrass(EnumTile tile)
        {
            return tile switch
            {
                EnumTile.Grass1 => true,
                _ => false
            };
        }

        private bool IsDirt(EnumTile tile)
        {
            return tile switch
            {
                EnumTile.Dirt1 => true,
                _ => false
            };
        }

        private bool IsSand(EnumTile tile)
        {
            return tile switch
            {
                EnumTile.Sand1 => true,
                _ => false
            };
        }

        private bool IsSnow(EnumTile tile)
        {
            return tile switch
            {
                EnumTile.Snow1 => true,
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
