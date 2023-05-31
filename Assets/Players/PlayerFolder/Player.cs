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
        
        public int Damage { get; private set; }
        
        public float speed;
        private Vector2 _dir = Vector2.zero;
        [SerializeField] private HealthBar healthBar;
        public Rigidbody2D rb;
        [SerializeField] protected new Camera camera;
        public Animator animator;
        private GameObject LaunchOffsetPlayer;
        private Rigidbody2D RblaunchOffsetPLayer;
        
        private Inventory.Inventory _inventory;
        public int money;
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
        public Text stoneText;
        public Text woodText;
        public GameObject canvas;
        // // // // // // // // // // // //

        public int nbTree;
        public int nbRock;
        private InputAction _farming;
        private bool _canBeFarm;
        private Collider2D _farmingElt;
        
        private InputAction _attack;
        private float _attackTimeCounter;
        

        private bool _walking;
        private float _lastMoveX;
        private float _lastMoveY;
        private double _attackTime;
        private bool _attacking;

        protected Player(int damage = 1, int health = 100, int speed = 1, int maxHealth = 100)
        {
            Damage = damage;
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
                
                _attackTime = 0.5;
                _attacking = false;
                
            }
        }

        protected void Start()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.SendRate = 40;
                PhotonNetwork.SerializationRate = 40;
                canvas.SetActive(true);
                healthBar.SetMaxHealth(MaxHealth);
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
            switch ((context.control as KeyControl)!.keyCode)
            {
                case Key.P:
                    PhotonNetwork.Instantiate("Zombie1", new Vector3(x + 5, y + 5, 0), Quaternion.identity);
                    break;
                case Key.O:
                    PhotonNetwork.Instantiate("Turret", new Vector3(x, y - 1, 0), Quaternion.identity);
                    break;
                case Key.I:
                    PhotonNetwork.Instantiate("PLayerWall", new Vector3(x, y - 1, 0), Quaternion.identity);
                    break;
                case Key.U:
                    PhotonNetwork.Instantiate("IA Farmer", new Vector3(x + 1, y + 1, 0), Quaternion.identity);
                    break;
            }
        }

        private void Give(InputAction.CallbackContext context)
        {
            money += 10;
            moneyText.text = money.ToString();
        }
        
        // upgrade item
        private void ItemUpgrade(InputAction.CallbackContext context)
        {
            switch ((context.control as KeyControl)!.keyCode)
            {
                case Key.Z:
                    money = _inventory.UpgradeItem(money, ItemEnum.Helmet);
                    helmetText.text = "LVL " + _inventory.Inv[0].Item2;
                    moneyText.text = money.ToString();
                    break;
                case Key.X:
                    money = _inventory.UpgradeItem(money, ItemEnum.ChestPlate);
                    chestPlateText.text = "LVL " + _inventory.Inv[1].Item2;
                    moneyText.text = money.ToString();
                    break;
                case Key.C:
                    money = _inventory.UpgradeItem(money, ItemEnum.Gloves);
                    glovesText.text = "LVL " + _inventory.Inv[2].Item2;
                    moneyText.text = money.ToString();
                    break;
                case Key.V:
                    money = _inventory.UpgradeItem(money, ItemEnum.Boots);
                    bootsText.text = "LVL " + _inventory.Inv[3].Item2;
                    moneyText.text = money.ToString();
                    break;
                case Key.B:
                    money = _inventory.UpgradeItem(money, ItemEnum.Sword);
                    Damage += _inventory.Inv[4].Item1.Damage;
                    swordText.text = "LVL " + _inventory.Inv[4].Item2;
                    moneyText.text = money.ToString();
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
                        _attackTime = 0.5;
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
                    _walking = true;
                    _lastMoveX = _dir.x;
                    _lastMoveY = _dir.y;
                }
                else if (_walking)
                {
                    animator.SetBool("Walking" , false);
                    animator.SetFloat("LastMoveX", _lastMoveX);
                    animator.SetFloat("LastMoveY",_lastMoveY);
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
                if (!_canBeFarm)
                {
                    if (col.transform.CompareTag("Rock") || col.transform.CompareTag("Tree"))
                    {
                        {
                            _farmingElt = col;
                            _canBeFarm = true;
                        }
                    }
                }
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (_canBeFarm)
            {
                {
                    _farmingElt = null;
                _canBeFarm = false;
                }
            }
        }
        
        private void Farming(InputAction.CallbackContext context)
        {
            if (_canBeFarm)
            {
                {
                    if (_farmingElt!.tag! == "Rock")
                    { Farming.Farming rock = new Farming.Farming("Rock");
                        nbRock += rock.Number;
                        stoneText.text = nbRock.ToString();
                        if (PhotonNetwork.IsMasterClient)
                        {
                            PhotonNetwork.Destroy(_farmingElt.gameObject);
                        }
                        else
                        {
                            GetComponent<PhotonView>().RPC("DestroyMe", RpcTarget.MasterClient, _farmingElt);
                        }
                    }
                    else if (_farmingElt!.tag! == "Tree")
                    {
                        Farming.Farming tree = new Farming.Farming("Tree");
                        nbTree += tree.Number;
                        woodText.text = nbTree.ToString();
                        if (PhotonNetwork.IsMasterClient)
                        {
                            PhotonNetwork.Destroy(_farmingElt.gameObject);
                        }
                        else
                        {
                            GetComponent<PhotonView>().RPC("DestroyMe", RpcTarget.MasterClient, _farmingElt);
                        }
                    }
                }
            }
        }
        
        [PunRPC]
        public void DestroyMe(Collider2D collider)
        {
            PhotonNetwork.Destroy(collider.gameObject);
        }
        
        private void Attack(InputAction.CallbackContext context)
        {
            animator.SetBool("Attack", true);
            _attacking = true;
        }
    }
}

