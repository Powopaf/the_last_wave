using System;
using UnityEngine;

namespace Players
{
    public class Player : MonoBehaviour
    {
        private int _health;
        private int _damage;
        private (int, int) _coordinate;
        //private Item[] _item_inv;
        //private ressource[] _ressource_inv;
        private string _name;
        private  int _heal;
        private int _speed;

        public Player()
        {
        
        }
        public Player(string name, int health = 1, int damage = 1, int heal = 5, int speed = 1)
        {
            _health = health;
            _damage = damage;
            _coordinate = (0,0);
            _name = name;
            _heal = heal;
            _speed = speed;
        }

        private static void Move()
        {
            throw new NotImplementedException("Move not implemented (check if someone did it)");
        }

        private static void Looting()
        {
            throw new NotImplementedException("Looting not implemented (check if someone did it)");
        }

        private  void Heal(int life)
        {
            _health += life * _heal;
        }
    }
}
