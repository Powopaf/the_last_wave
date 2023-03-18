using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Players.PlayerFolder;
using Scenes.ATH;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;


namespace Players
{
    public abstract class Player : MonoBehaviour
    {
        private int Health { get; set; }
        private int MaxHealth { get; }
        private int Damage { get; set; }
        private List<Item.Item> _item_inv;
        private (string,int)[] _ressource_inv;
        private string _name;
        private  int _heal;
        public float speed;
        private Vector2 dir=Vector2.zero;
        [SerializeField] private HealthBar healthBar;
        public Rigidbody2D rb;
        [SerializeField] protected new Camera camera;
        public Animator animator;
        private GameObject LaunchOffsetPlayer;
        private Rigidbody2D RblaunchOffsetPLayer;
        
        
        private PlayerInputAction _playerControl;
        private InputAction _move;
        private InputAction _sight;
        private Vector2 pointerInput;
        private Vector3 mousepos;
        private Playersight _playersight;
        private PlayerWeaponAttack _playerWeaponAttack;

        public Player(int health = 100, int damage = 1,
            int speed = 1, int maxHealth = 100, int heal = 1, string name = "")
        {
            MaxHealth = maxHealth;
            Health = health;
            Damage = damage;
            _name = name;
            _heal = heal;
            this.speed = speed;
            _item_inv = new List<Item.Item>();
            _ressource_inv = new[] { ("wood", 0), ("stone", 0), ("iron", 0) };
        }

        protected void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            _playerControl = new PlayerInputAction();
            _playersight = GetComponentInChildren<Playersight>();
            _playerWeaponAttack = GetComponentInChildren<PlayerWeaponAttack>();
            camera=Camera.main;
            animator = GetComponent<Animator>();
        }

        protected void Start()
        {
            healthBar.SetMaxHealth(MaxHealth);
            healthBar.SetHealth(MaxHealth);

        }

        protected void OnEnable()
        {
            _move = _playerControl.Player.Move;
            _move.Enable();

            _sight = _playerControl.Player.PointerPosition;
            _sight.Enable();


        }
        protected void OnDisable()
        {
            _move.Disable();
            _sight.Disable();
        }
        

        protected void Update()
        { 
            animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
           animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
           healthBar.SetHealth(Health);
           dir = _move.ReadValue<Vector2>();
           pointerInput = GetPointerInput();

        }
        

        protected void FixedUpdate()
        {
            rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
            healthBar.SetHealth(Health);
            _playersight.PointerPosition = pointerInput;
            _playerWeaponAttack.PointerPosition = pointerInput;

        }
        private Vector2 GetPointerInput()
        {
            mousepos = _sight.ReadValue<Vector2>();
            mousepos.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(mousepos);  
          
            
            //  Vector3 direction = worldpmousepos - LaunchOffsetPlayer.transform.position; 
          //  float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           // RblaunchOffsetPLayer.rotation = angle;
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
