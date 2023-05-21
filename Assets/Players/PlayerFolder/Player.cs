using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Photon.Pun;
using Scenes.ATH;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using World;

namespace Players.PlayerFolder
{
    public abstract class Player : MonoBehaviour
    {
        private int Health { get; set; }
        private int MaxHealth { get; }
        private int Damage { get; set; }

        private (string,int)[] _ressource_inv;
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
        public GameObject Mark;
        public GameObject CanvasName;
        public TMP_Text Name;

        public int nbTree = 0;
        public int nbRock = 0;
        public int nbGold = 0;

        public Player(int health = 100, int damage = 1,
            int speed = 1, int maxHealth = 100, int heal = 1, string name = "")
        {
            MaxHealth = maxHealth;
            Health = health;
            Damage = damage;
            _name = name;
            _heal = heal;
            this.speed = speed;
            _ressource_inv = new[] { ("wood", 0), ("stone", 0), ("iron", 0) };
        }

        protected void Awake()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                rb = GetComponent<Rigidbody2D>();
                _playerControl = new PlayerInputAction();
                _playersight = GetComponentInChildren<Playersight>();
                camera = Camera.main;
                animator = GetComponent<Animator>();
            }
        }

        protected void Start()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.SendRate = 40;
                PhotonNetwork.SerializationRate = 40;
                healthBar.SetMaxHealth(MaxHealth);
                healthBar.SetHealth(MaxHealth);
            }
            else
            {
                CanvasName.SetActive(true);
                Name.text = GetComponent<PhotonView>().Controller.NickName;
            }
        }

        protected void OnEnable()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                _move = _playerControl.Player.Move;
                _move.Enable();

                _sight = _playerControl.Player.PointerPosition;
                _sight.Enable();
            }
        }

        protected void OnDisable()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                _move.Disable();
                _sight.Disable();
            }
        }

        protected void Update()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
                animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
                healthBar.SetHealth(Health);
                dir = _move.ReadValue<Vector2>();
                pointerInput = GetPointerInput();
            }
        }

        protected void FixedUpdate()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
                healthBar.SetHealth(Health);
                _playersight.PointerPosition = pointerInput;
            }
        }

        private Vector2 GetPointerInput()
        {
            mousepos = _sight.ReadValue<Vector2>();
            mousepos.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(mousepos);
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

