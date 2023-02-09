using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : Item
{
    private int _buff;
    private Item _itemImplementation;

    public Potions(int durability = 1, int buff = 1)
    {
        _durability = durability;
        _buff = buff;
    }

    protected override void UpdateMe(int i)
    {
        _durability -= 1;
    }
}
