using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Protections : Item
{
    private int _shield;

    public Protections(int durability = 1, int shield = 1)
    {
        _durability = durability;
        _shield = shield;
    }

    private void UpdateProtection()
    {
        _durability += 5;
        _shield += 5;
    }

    protected override void UpdateMe(int i)
    {
        if (i < 0)
        {
            _durability -= 1;
        }
        else
        {
            _durability += 1;
        }
    }
}
