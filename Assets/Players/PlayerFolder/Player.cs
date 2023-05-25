using UnityEngine.UI;
using ATH.HealthBar;
using Photon.Pun;
using Players.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

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
                _spawnZombie.performed += 
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
            }
        }

        protected void Update()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
                animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
                healthBar.SetHealth(Health);
            }
        }

        protected void FixedUpdate()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                _dir = _move.ReadValue<Vector2>();
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

