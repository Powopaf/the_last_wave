#nullable enable
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
        
        public float speed;
        private Vector2 _dir = Vector2.zero;
        [SerializeField] private HealthBar healthBar;
        public Rigidbody2D rb;
        [SerializeField] protected new Camera camera;
        public Animator animator;
        private GameObject _launchOffsetPlayer;
        private Rigidbody2D _rblaunchOffsetPLayer;


        private PlayerInputAction _playerControl;
        private InputAction _move;
        public GameObject CanvasName;
        public TMP_Text Name;

        //Farimngcode
        public int nbTree;
        public int nbRock;
        private InputAction _farming;
        private bool _canbefarm;
        private Collider2D? _farmingElt;

        public Player(int health = 100, int speed = 1, int maxHealth = 100)
        {
            MaxHealth = maxHealth;
            Health = health;
            this.speed = speed;
        }

        protected void Awake()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                rb = GetComponent<Rigidbody2D>();
                _playerControl = new PlayerInputAction();
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

                /////////////////////////////////////Farmingcode
                _farming = _playerControl.Player.Farming;
                _farming.performed += Farming; 
                _farming.Enable();
            }
        }

        protected void OnDisable()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                _move.Disable();
                _farming.Disable();
            }
        }

        protected void Update()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
                animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
                healthBar.SetHealth(Health);
                _dir = _move.ReadValue<Vector2>();
            }
        }

        protected void FixedUpdate()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                rb.velocity = new Vector2(_dir.x * speed, _dir.y * speed);
                healthBar.SetHealth(Health);
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
     
        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.transform.CompareTag("Rock") || col.transform.CompareTag("Tree"))
            {
                if (!_canbefarm)
                {
                    _farmingElt = col;
                    _canbefarm = true;
                }
                
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (_canbefarm)
            {
                _farmingElt = null;
                _canbefarm = false;
            }
        }
        
        private void Farming(InputAction.CallbackContext context)
        {
            if (_canbefarm)
            {
                if (_farmingElt!.tag! == "Rock")
                {
                    Farming.Farming rock=new Farming.Farming("Rock");
                    nbRock += rock.Number;
                    Debug.Log(nbRock);
                   
                }
                else if (_farmingElt!.tag! =="Tree")
                {
                    Farming.Farming tree = new Farming.Farming("Tree");
                    nbTree += tree.Number;
                    Debug.Log(nbTree);
                }
                Destroy(_farmingElt.gameObject);
            }
        }
    }
}

