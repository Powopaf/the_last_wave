using System;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class Player : MonoBehaviour
    {
        private int Health { get; set; }
        private int Damage { get; set; }
        private (int X, int Y) _coordinate;
        private List<Item> _item_inv;
        private (string,int)[] _ressource_inv;
        private string _name;
        private  int _heal;
        private int _speed;
        
        public Player(int health = 1, int damage = 1,
            int speed = 1, int heal = 1, string name = "")
        {
            Health = health;
            Damage = damage;
            _coordinate = (0,0);
            _name = name;
            _heal = heal;
            _speed = speed;
            _item_inv = new List<Item>();
            _ressource_inv = new[] { ("wood", 0), ("stone", 0), ("iron", 0) };
        }
        
        private void CoordUpdate(int x, int y)
        {
            _coordinate.X += x;
            _coordinate.Y += y;
        }
        
        private void Looting(Item[] loot)
        {
            int i = 0;
            while (_item_inv.Count <= 9 && i < loot.Length)
            {
                _item_inv.Add(loot[i]);
            }
        }

        private void Looting(Item item)
        {
            if (_item_inv.Count == 9)
            {
                return;
            }
            _item_inv.Add(item);
        }

        private void Heal(int life)
        {
            Health += life * _heal;
        }
    }
}
