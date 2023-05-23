using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Photon.Pun;
using Players.Inventory;
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
        private GameObject LaunchOffsetPlayer;
        private Rigidbody2D RblaunchOffsetPLayer;
        
        private Inventory.Inventory _inventory;
        private int _money;
        private InputAction _giveMoney;
        private InputAction _upgradeInv;
        private VisualInventory _visualInventory;


        private PlayerInputAction _playerControl;
        private InputAction _move;
        public GameObject CanvasName;
        public TMP_Text Name;

        public int nbTree;
        public int nbRock;
        private InputAction _farming;
        private bool _canbefarm;
        private Collider2D _farmingElt;

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

                // touche pour l'inv
                _upgradeInv = _playerControl.Player.UpgradeItem;
                _upgradeInv.performed += ItemUpgrade;
                _upgradeInv.Enable();
                // touche pour give
                _giveMoney = _playerControl.Player.Give;
                _giveMoney.performed += Give;
                _giveMoney.Enable();
                
                _farming = _playerControl.Player.Farming;
                _farming.performed += Farming; 
                _farming.Enable();
            }
        }
        
        private void Give(InputAction.CallbackContext context) => _money += 10;
        
        // upgrade item
        private void ItemUpgrade(InputAction.CallbackContext context)
        {
            var index = new GetItem().GetInv;
            switch (context.ReadValue<Key>())
            {
                case Key.Digit1:
                    _inventory.UpgradeItem(_money, ItemEnum.Helmet);
                    _visualInventory.UpdateText(_inventory.Inv[index[ItemEnum.Helmet]].Item2, ItemEnum.Helmet);
                    break;
                case Key.Digit2:
                    _inventory.UpgradeItem(_money, ItemEnum.ChestPlate);
                    _visualInventory.UpdateText(_inventory.Inv[index[ItemEnum.ChestPlate]].Item2, ItemEnum.ChestPlate);
                    break;
                case Key.Digit3:
                    _inventory.UpgradeItem(_money, ItemEnum.Gloves);
                    _visualInventory.UpdateText(_inventory.Inv[index[ItemEnum.Gloves]].Item2, ItemEnum.Gloves);
                    break;
                case Key.Digit4:
                    _inventory.UpgradeItem(_money, ItemEnum.Boots);
                    _visualInventory.UpdateText(_inventory.Inv[index[ItemEnum.Boots]].Item2, ItemEnum.Boots);
                    break;
                case Key.Digit5:
                    _inventory.UpgradeItem(_money, ItemEnum.Sword);
                    _visualInventory.UpdateText(_inventory.Inv[index[ItemEnum.Sword]].Item2, ItemEnum.Sword);
                    break;
            }
        }

        protected void OnDisable()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                _move.Disable();
                _upgradeInv.Disable();
                _giveMoney.Disable();
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

