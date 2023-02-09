using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : Item
{
    private int _buff;

    public Potions(int durability = 1, int buff = 1)
    {
        _durability = durability;
        _buff = buff;
    }
}
