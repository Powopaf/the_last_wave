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
                EnumTile.Grass1 => true,
                _ => false
            };
        }

        public static bool IsSnow(EnumTile tile)
        {
            return tile switch
            {
                EnumTile.Snow1 => true,
                _ => false
            };
        }
    }
}