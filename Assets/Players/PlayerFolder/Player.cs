using UnityEngine.UI;
using ATH.HealthBar;
using Photon.Pun;
using Players.Inventory;
using Players.Item;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Players.PlayerFolder
{
    public abstract class Player : MonoBehaviour
    {
        public int Health { get; set; }
        public int MaxHealth { get; }
        
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
        
        //spawn 1 zombies
        private InputAction _spawnZombie;
        private PlayerInputAction _playerControl;
        private InputAction _move;
        public GameObject CanvasName;
        public TMP_Text Name;
        
        // text for UI inventory and money
        public Text helmetText;
        public Text chestPlateText;
        public Text glovesText;
        public Text bootsText;
        public Text swordText;
        public Text moneyText;
        // // // // // // // // // // // //

        public int nbTree;
        public int nbRock;
        private InputAction _farming;
        private bool _canbefarm;
        private Collider2D _farmingElt;
        
        //
        private InputAction _attack;
        private float _attackTimeCounter;
        
        private bool Walking;
        private float _LastMoveX;
        private float _LastMoveY;
        private float _attackTime;
        private bool _attacking;
        public Player(int health = 100, int speed = 1, int maxHealth = 100)
        {
            MaxHealth = maxHealth;
            Health = health;
            this.speed = speed;
            _inventory = new Inventory.Inventory();
            
        }

        protected void Awake()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                rb = GetComponent<Rigidbody2D>();
                _playerControl = new PlayerInputAction();
                camera = Camera.main;
                animator = GetComponent<Animator>();
                
                _attackTime = 1;
                _attacking = false;
            }
        }

        protected void Start()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.SendRate = 40;
                PhotonNetwork.SerializationRate = 40;
                healthBar.SetMaxHealth(MaxHealth);
                helmetText.text = _inventory.Inv[0].Item2.ToString();
                chestPlateText.text = _inventory.Inv[1].Item2.ToString();
                glovesText.text = _inventory.Inv[2].Item2.ToString();
                bootsText.text = _inventory.Inv[3].Item2.ToString();
                swordText.text = _inventory.Inv[4].Item2.ToString();
                moneyText.text = _money.ToString();
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
                _spawnZombie = _playerControl.Player.Spawn;
                _spawnZombie.performed += Spawn;
                _spawnZombie.Enable();
                
                _move = _playerControl.Player.Move;
                _move.Enable();
                
                // touche pour give
                _giveMoney = _playerControl.Player.Give;
                _giveMoney.performed += Give;
                _giveMoney.Enable();

                _upgradeInv = _playerControl.Player.Upgrade;
                _upgradeInv.performed += ItemUpgrade;
                _upgradeInv.Enable();
                
                _farming = _playerControl.Player.Farming;
                _farming.performed += Farming; 
                _farming.Enable();
                
                _attack = _playerControl.Player.Attack;
                _attack.performed += Attack;
                _attack.Enable();
            }
        }

        private void Spawn(InputAction.CallbackContext context)
        {
            var position = rb.transform.position;
            var x = position.x;
            var y = position.y;
            if ((context.control as KeyControl)!.keyCode == Key.P)
            {
                PhotonNetwork.Instantiate("Zombie1", new Vector3(x + 1, y + 1, -1), Quaternion.identity);
            }
            else if((context.control as KeyControl)!.keyCode == Key.O)
            {
                PhotonNetwork.Instantiate("Turret", new Vector3(x + 1, y, -1), Quaternion.identity);
            }
        }

        private void Give(InputAction.CallbackContext context)
        {
            _money += 10;
            moneyText.text = _money.ToString();
        }
        
        // upgrade item
        private void ItemUpgrade(InputAction.CallbackContext context)
        {
            switch ((context.control as KeyControl)!.keyCode)
            {
                case Key.Z:
                    _money = _inventory.UpgradeItem(_money, ItemEnum.Helmet);
                    helmetText.text = _inventory.Inv[0].Item2.ToString();
                    moneyText.text = _money.ToString();
                    break;
                case Key.X:
                    _money = _inventory.UpgradeItem(_money, ItemEnum.ChestPlate);
                    chestPlateText.text = _inventory.Inv[1].Item2.ToString();
                    moneyText.text = _money.ToString();
                    break;
                case Key.C:
                    _money = _inventory.UpgradeItem(_money, ItemEnum.Gloves);
                    glovesText.text = _inventory.Inv[2].Item2.ToString();
                    moneyText.text = _money.ToString();
                    break;
                case Key.V:
                    _money = _inventory.UpgradeItem(_money, ItemEnum.Boots);
                    bootsText.text = _inventory.Inv[3].Item2.ToString();
                    moneyText.text = _money.ToString();
                    break;
                case Key.B:
                    _money = _inventory.UpgradeItem(_money, ItemEnum.Sword);
                    swordText.text = _inventory.Inv[4].Item2.ToString();
                    moneyText.text = _money.ToString();
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
                _spawnZombie.Disable();
                _attack.Disable();
            }
        }

        protected void Update()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                animator.SetFloat("X", _dir.x);
                animator.SetFloat("Y", _dir.y);
                if (_attacking)
                {
                    _attackTime -= Time.deltaTime;
                    if (_attackTime <= 0)
                    {
                        animator.SetBool("Attack",false);
                        _attackTime = 1;
                        _attacking = false;
                    }
                }
                
            }
        }

        protected void FixedUpdate()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                _dir = _move.ReadValue<Vector2>();
                rb.velocity = new Vector2(_dir.x * speed, _dir.y * speed);
                if (rb.velocity != Vector2.zero)
                {
                    animator.SetBool("Walking", true);
                    Walking = true;
                    _LastMoveX = _dir.x;
                    _LastMoveY = _dir.y;

                }
                else if (Walking)
                {
                    animator.SetBool("Walking" , false);
                    animator.SetFloat("LastMoveX", _LastMoveX);
                    animator.SetFloat("LastMoveY",_LastMoveY);
                }
            }
        }

        public bool ZombieDamageOnPlayer(int damage)
        {
            int defence = 0;
            foreach ((IItem, int) item in _inventory.Inv)
            {
                defence += item.Item2;
            }
            // ReSharper disable once IntDivisionByZero
            Health -= damage / (defence / 2);
            healthBar.SetHealth(Health);
            return Health <= 0;
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
                PhotonNetwork.Destroy(_farmingElt.gameObject);
            }
        }
        
        private void Attack(InputAction.CallbackContext context)
        {
            animator.SetBool("Attack", true);
            _attacking = true;
        }
    }
}

