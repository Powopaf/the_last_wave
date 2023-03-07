using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace Players
{
    public abstract class Player : MonoBehaviour
    {
        private int Health { get; set; }
        private int Damage { get; set; }
        //private (int X, int Y) _coordinate;
        private List<Item> _item_inv;
        private (string,int)[] _ressource_inv;
        private string _name;
        private  int _heal;
        public float _speed;
        protected Vector2 dir;
        protected int move = -1;
        public Rigidbody2D rb;
        [SerializeField] private Camera camera;
        
        public Player(int health = 1, int damage = 1,
            int speed = 1, int heal = 1, string name = "")
        {
            Health = health;
            Damage = damage;
            //_coordinate = (0,0);
            _name = name;
            _heal = heal;
            _speed = speed;
            _item_inv = new List<Item>();
            _ressource_inv = new[] { ("wood", 0), ("stone", 0), ("iron", 0) };
        }

        protected void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            camera=Camera.main;
        }

        protected void MovePlayer()
        {
            rb.MovePosition(rb.position + dir * (_speed * Time.deltaTime));
        }

        protected void Update()
        {
            Vector3 mousepos = Input.mousePosition;  
            mousepos.z = camera.nearClipPlane;
            Vector3 worldpmousepos = camera.ScreenToWorldPoint(mousepos);
            Vector3 direction = worldpmousepos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
            rb.rotation = angle;
        }
        

        protected void FixedUpdate()
        {
            dir.x = Input.GetAxis("Horizontal");
            dir.y = Input.GetAxis("Vertical");
            MovePlayer();
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

        public void ZombieDamageOnPlayer(int damage)
        {
            if (Health - damage > 0)
            {
                Health -= damage;
                Debug.Log("Aie!!!!!");
            }
            else
            {
                Debug.Log("the player died!!!"); // To see the effect pf the Zombie Attack
            }
        }
    }
}
