namespace World
{
    public enum EnumTile
    {
        ///  WallBorder and Tile None
        NoTile = 0,
        WallBorderMap,
        /// Dirt Tile
        Dirt1,
        Dirt2, // 1 2 <- In this order
        Dirt3, // 3 4
        Dirt4,
        /// Sand Tile
        Sand1, // 1 2 3 <- In this order
        Sand2, // 4 5 6
        Sand3,
        Sand4,
        Sand5,
        Sand6,
        SandDefault1, // 1 2 <- In this order
        SandDefault2, // 3 4
        SandDefault3,
        SandDefault4,
        /// Snow Tile
        Snow1, // 1 _ <- in this order _ == no tile on this patern
        Snow2, // 2 3
        Snow3,
        SnowDefault1,
        SnowDefault2,
        SnowDefault3,
        SnowDefault4,
        SnowSideBot1,
        SnowSideBot2,
        SnowSideLeft,
        SnowSideRight,
        SnowSideTop1,
        SnowSideTop2,
        /// Water tile
        Water1,
        /// Grass Tile
        DefaultGrass1,
        DefaultGrass2,
        DefaultGrass3,
        DefaultGrass4,
        Grass1, // 1 2 3 4 <- In this order
        Grass2, // 5 6 7 8 
        Grass3,
        Grass4,
        Grass5,
        Grass6,
        Grass7,
        Grass8,
        GrassSideLeft,
        GrassSideRight,
        GrassSideTop,
        GrassSideBot,
    }
}