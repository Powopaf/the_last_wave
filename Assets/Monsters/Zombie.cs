using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Zombie
{
    private int _health;
    private int _damage;
    private string _name;
    //private Item[] _loot;
    private string[] _target;
    private (int, int) _coordinate;
    private int _speed;

    protected Zombie(string name = "", string[] target = null,
        int health = 1, int damage = 1, int speed = 1)
    {
        _name = name;
        _target = target;
        _health = health;
        _damage = damage;
    }
}
