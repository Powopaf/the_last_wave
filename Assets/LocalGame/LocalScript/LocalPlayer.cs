using ATH.HealthBar;
using Players.Inventory;
using Players.Farming;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LocalGame.LocalScript
{
    public abstract class LocalPlayer : MonoBehaviour
    {
        private int Health { get; set; }
        private int MaxHealth { get; }
        private int Damage { get; }
        // player inv
        private Inventory _inventory;
        private int _money;
        private InputAction _giveMoney;
        private InputAction _upgradeInv;
        /////////////////////

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
       

        //Farimngcode
        public int nbTree = 0;
        public int nbRock = 0;
        private InputAction _farming;
        private bool Canbefarm = false;
        private Collider2D? _farmingElt;
        //
        //Attack
        private InputAction _attack;
        private float _attackTimeCounter;
        

        protected LocalPlayer(int health = 100, int damage = 1, int speed = 1, int maxHealth = 100, int heal = 1)
        {
            MaxHealth = maxHealth;
            Health = health;
            Damage = damage;
            _heal = heal;
            this.speed = speed;
            _inventory = new Inventory();
            _money = 1;
        }

        protected void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            _playerControl = new PlayerInputAction();
            camera = GameObject.FindWithTag("Camera").GetComponent<Camera>();
            animator = GetComponent<Animator>();
            _attackTimeCounter = 2;
        }

        protected void Start()
        {
            healthBar.SetMaxHealth(MaxHealth);
            ///   _visualInventory.InitText();
        }

        protected void OnEnable()
        {
            _move = _playerControl.Player.Move;
            _move.Enable();


           
            
            /////////////////////////////////////Farmingcode
            _farming = _playerControl.Player.Farming;
            _farming.performed += Farming; 
            _farming.Enable();
            // touche pour l'inv
            // touche pour give
            _giveMoney = _playerControl.Player.Give;
            _giveMoney.performed += Give;
            _giveMoney.Enable();
            ///Attack
            _attack = _playerControl.Player.Attack;
            _attack.performed += Attack;
            _attack.Enable();
        }
        // give money
        private void Give(InputAction.CallbackContext context) => _money += 10;
        
        // upgrade item
        private void ItemUpgrade(InputAction.CallbackContext context)
        {
            _money = context.ReadValue<Key>() switch
            {
                Key.Digit1 => _inventory.UpgradeItem(_money, ItemEnum.Helmet),
                Key.Digit2 => _inventory.UpgradeItem(_money, ItemEnum.ChestPlate),
                Key.Digit3 => _inventory.UpgradeItem(_money, ItemEnum.Gloves),
                Key.Digit4 => _inventory.UpgradeItem(_money, ItemEnum.Boots),
                Key.Digit5 => _inventory.UpgradeItem(_money, ItemEnum.Sword),
                _ => _money
            };
        }
        // // // // // // // //
        
        protected void OnDisable()
        {
            _move.Disable();

            //Farmingcode
            _farming.Disable();
            _upgradeInv.Disable();
            _giveMoney.Disable();
            _attack.Disable();
            
        }

        protected void Update()
        {
            animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
            dir = _move.ReadValue<Vector2>();
            
            //attack
            animator.SetFloat("LastMoveX", dir.x);
            animator.SetFloat("LastMoveY",dir.y);
        }

        protected void FixedUpdate()
        {
            rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
        }
        
        public void ZombieDamageOnPlayer(int damage)
        {
            if (Health - damage > 0)
            {
                Health -= damage;
                Debug.Log($"Damage: {damage} | Health before : {Health + damage} | Health after: {Health}");
            }
            else
            {
                Debug.Log("the player died!!!"); // To see the effect pf the Zombie Attack
            }
        }
        //////////////////////////////////////////////////////////////////////////////Farming code
        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.transform.CompareTag("Rock") || col.transform.CompareTag("Tree"))
            {
                if (!Canbefarm)
                {
                    _farmingElt = col;
                    Canbefarm = true;
                }
                
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (Canbefarm)
            {
                _farmingElt = null;
                Canbefarm = false;
            }
        }
        
        private void Farming(InputAction.CallbackContext context)
        {
            if (Canbefarm)
            {
                if (_farmingElt.tag == "Rock")
                {
                    Farming rock=new Farming("Rock");
                    nbRock += rock.Number;
                    Debug.Log(nbRock);
                   
                }
                else if (_farmingElt.tag =="Tree")
                {
                    Farming tree = new Farming("Tree");
                    nbTree += tree.Number;
                    Debug.Log(nbTree);
                }
                Destroy(_farmingElt.gameObject);
            }
            
        }
       /////////////////////////////////////////////////////////////////////////////////////////
       //Attack
       private void Attack(InputAction.CallbackContext context)
       {
          
           animator.SetBool("Attack", true);
           _attackTimeCounter = 2;
       }
    }
}