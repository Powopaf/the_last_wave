using System;
using World;


public static class Spawn
{
    public static bool Placable(MapDefinition mapDefinition, int x, int y)
    {
        if (!(x >= 0 && x < mapDefinition.Width && y >= 0 && y < mapDefinition.Height))
        {
            return false;
        }
        var map = mapDefinition.Map;
        if (map[x,y].IsWall)
        {
            return false;
        }

        return true;
    }

    public static int Length(MapDefinition mapDefinition, int x, int y, string direction)
    {
        int res = 0;
        int vectorX = 0;
        int vectorY = 0;
        int X = x;
        int Y = y;

        switch (direction)
        {
            case "north":
                vectorY = 1;
                break;
            case "south":
                vectorY = -1;
                break;
            case "east":
                vectorX = 1;
                break;
            case "west":
                vectorX = -1;
                break;
            default:
                throw new ArgumentException();
        }

        while (x >= 0 && X < mapDefinition.Width && Y >= 0 && Y < mapDefinition.Height && 
               !mapDefinition.Map[X, Y].IsWall)
        {
            X += vectorX;
            Y += vectorY;
            res += 1;
        }

        return res;
    }

    public static bool IsMainIsland(int height, int width)
    {
        if ((height + width) / 2 >= 400)
        {
            return true;
        }

        return false;
    }

    public static bool IsSpawnable(MapDefinition map, int x, int y)
    {
        if (!(x >= 0 && x < map.Width && y >= 0 && y < map.Height))
        {
            return false;
        }
        if (!map.Map[x,y].IsWall && !map.Map[x,y].HaveRock && !map.Map[x,y].HaveTree)
        {
            return true;
        }

        return false;
    }
}