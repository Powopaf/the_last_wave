using System;
using UnityEngine;
public class Zombie4 : Zombie
{
    public Zombie4() :
        base("Zombie4", 
            new []{"Player", "Core"},
            100, 85, 70) {}

    protected override void Start()
    {
        throw new NotImplementedException();
    }

    protected override void FixedUpdate()
    {
        throw new NotImplementedException();
    }
    protected override void Update()
    {
        throw new NotImplementedException();
    }


    protected override void ZombieMovement(Vector2 direction)
    {
        throw new NotImplementedException();
    }
}