using System;
using System.Collections.Generic;
using Photon.Pun;
using Scenes.ATH;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using World;


namespace Players.PlayerFolder
{
    public abstract class LocalPlayer : MonoBehaviour
    {
        private int Health { get; set; }
        private int MaxHealth { get; }
        private int Damage { get; set; }
        private List<Item.Item> _item_inv;
        private (string, int)[] _ressource_inv;
        private string _name;
        private int _heal;
        public float speed;
        private Vector2 dir = Vector2.zero;
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

        public int nbTree = 0;
        public int nbRock = 0;
        public int nbGold = 0;
        private InputAction _farming;

        public LocalPlayer(int health = 100, int damage = 1,
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
            camera = GameObject.FindWithTag("Camera").GetComponent<Camera>();
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

            _farming = _playerControl.Player.Farming;
            _farming.Enable();


        }

        protected void OnDisable()
        {
            _move.Disable();
            _sight.Disable();
            _farming.Disable();
        }

        protected void Update()
        {
            animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
            healthBar.SetHealth(Health);
            dir = _move.ReadValue<Vector2>();
        }

        protected void FixedUpdate()
        {
            rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
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
        public void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.CompareTag("Rock") || col.transform.CompareTag("Tree") ||
                col.transform.CompareTag("SnowTree"))
            {
                TileDefinition[,] map = GameObject.FindWithTag("LocalMap").GetComponent<LocalMap>()
                    ._mapDefinition.Map;
                switch (col.transform.tag)
                {
                    case "SnowTree":
                        Farming tree = new Farming("Tree");
                        nbTree += tree.number;
                        (double X, double Y) = (Math.Truncate(transform.position.x),
                            Math.Truncate(transform.position.y));
                        break;
                    case "Rock":
                        Farming rock1 = new Farming("Rock");
                        nbRock += rock1.number;
                        Debug.Log($"number of rock :{nbRock}");
                        if (Keyboard.current.qKey.wasPressedThisFrame)
                        { 
                            Destroy(col.gameObject);
                        }
                        break;
                }
            }
        }



    }
}