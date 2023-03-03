using System;
using System.Collections.Generic;
using UnityEngine;


namespace Players
{
    public abstract class Player : MonoBehaviour
    {
        public int Health;
        private int Damage { get; set; }
        private (int X, int Y) _coordinate;
        private List<Item> _item_inv;
        private (string,int)[] _ressource_inv;
        private string _name;
        private  int _heal;
        public float _speed;
        public Rigidbody2D rb;
        protected Vector2 dir;
        protected int move = -1;

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
        
        protected abstract void FixedUpdate();
        protected abstract void MovePlayer();
        
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

        public void TakeDamage(int damage)
        {
            if (Health - damage > 0)
            {
                Health -= damage;
            }
            else
            {
                Health = 0; // The player need to die !
            }
        }
    }
}
