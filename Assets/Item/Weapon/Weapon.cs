using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Weapon : Item
{
    private int _damage;

    public Weapon(int durability, int damage = 1)
    {
        _damage = damage;
        _durability = durability;
    }

    private void UpdateWeapon()
    {
        _damage += 5;
        _durability += 5;
    }
}