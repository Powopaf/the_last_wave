using System;

public class Zombie4 : Zombie
{
    public Zombie4() :
        base("Zombie4", 
            new []{"Player", "Core"},
            100, 85, 70) {}

    private static void Movement()
    {
        throw new NotImplementedException();
    }
}