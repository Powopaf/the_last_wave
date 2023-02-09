using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1 : Zombie
{
    public Zombie1() :
        base("Zombie1", 
            new []{"Player", "Core"},
            100, 20, 30) {}

    private static void Movement()
    {
        throw new NotImplementedException();
    }
}
