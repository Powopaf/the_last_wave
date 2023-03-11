using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Scenes.ATH;
using UnityEngine;
using UnityEngine.Serialization;


namespace Players
{
    public abstract class Player : MonoBehaviour
    {
        private int Health { get; set; }
        private int MaxHealth { get; }
        private int Damage { get; set; }
        //private (int X, int Y) _coordinate;
        private List<Item.Item> _item_inv;
        private (string,int)[] _ressource_inv;
        private string _name;
        private  int _heal;
        public float speed;
        private Vector2 dir;
        [SerializeField] private HealthBar healthBar;
        public Rigidbody2D rb;
        [SerializeField] protected new Camera camera;
        public Animator animator;
        private GameObject LaunchOffsetPlayer;
        private Rigidbody2D RblaunchOffsetPLayer;
        
        public Player(int health = 100, int damage = 1,
            int speed = 1, int maxHealth = 100, int heal = 1, string name = "")
        {
            MaxHealth = maxHealth;
            Health = health;
            Damage = damage;
            //_coordinate = (0,0);
            _name = name;
            _heal = heal;
            this.speed = speed;
            _item_inv = new List<Item.Item>();
            _ressource_inv = new[] { ("wood", 0), ("stone", 0), ("iron", 0) };
        }

        protected void Start()
        {
            healthBar.SetMaxHealth(MaxHealth);
            healthBar.SetHealth(MaxHealth);
            rb = GetComponent<Rigidbody2D>();
            camera=Camera.main;
            LaunchOffsetPlayer = GameObject.FindWithTag("PlayerLaunchOffset");
            RblaunchOffsetPLayer = LaunchOffsetPlayer.GetComponent<Rigidbody2D>();
            healthBar.SetMaxHealth(MaxHealth);
            healthBar.SetHealth(MaxHealth);

        }

        private void MovePlayer()
        {
            rb.MovePosition(rb.position + dir * (speed * Time.deltaTime));
        }

        protected void Update()
        {
            
            animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
           animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
            healthBar.SetHealth(Health);
          //  Vector3 mousepos = Input.mousePosition;
           // mousepos.z = camera.nearClipPlane;
          //  Vector3 worldpmousepos = camera.ScreenToWorldPoint(mousepos);
            //Vector3 direction = worldpmousepos - LaunchOffsetPlayer.transform.position;
           // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           // RblaunchOffsetPLayer.rotation = angle;
           
        }
        

        protected void FixedUpdate()
        {
            dir.x = Input.GetAxis("Horizontal");
            dir.y = Input.GetAxis("Vertical");
            MovePlayer();
            healthBar.SetHealth(Health);
        }
        
        private void Looting(Item.Item[] loot)
        {
            int i = 0;
            while (_item_inv.Count <= 9 && i < loot.Length)
            {
                _item_inv.Add(loot[i]);
            }
        }

        private void Looting(Item.Item item)
        {
            if (_item_inv.Count == 9)
            {
                return;
            }
            _item_inv.Add(item);
        }

        private void Heal(int life)
        {
            if (life * _heal >= MaxHealth)
            {
                Health = MaxHealth;
            }
            else
            {
                Health += life * _heal;
            }
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
