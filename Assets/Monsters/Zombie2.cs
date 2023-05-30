using Pathfinding;
using Photon.C__script;
using UnityEngine;

namespace Monsters
{
    public class Zombie2 : Zombie
    {
        public float playerdistance = 10;
        public GameObject zombie2Projectile;
        public GameObject launchOffset;
        private Rigidbody2D _launchOffsetRigidbody2D;
        private float _zombieWeaponRecharging = 1 ;
        private static readonly int X = Animator.StringToHash("X");
        private static readonly int Y = Animator.StringToHash("Y");

        public Zombie2() : base(new []{"Assassin","Farmer","Survivor","Worker"}, 50, 15, 100) { }

        protected override void Awake()
        {
            lvl = GameObject.FindWithTag("ZombieLVL").GetComponent<ZombieLVL>().lvl;
            SetLevel();
            playerdistance = 10;
            _launchOffsetRigidbody2D = launchOffset.GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            AI=GetComponent<AIPath>();
            AIsetter.target=GameObject.FindWithTag("Core").transform;
        }

        protected void Update()
        {
            AI.canMove = true;
            if (( (Vector2)transform.position-(Vector2)AIsetter.target.position).magnitude<=playerdistance)
            {
                AI.canMove = false;
            }
            if (_zombieWeaponRecharging <= 0)
            {
                if ((AIsetter.target.position - transform.position).magnitude < playerdistance + 2)
                {
                    Vector3 direction = AIsetter.target.position - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    _launchOffsetRigidbody2D.rotation = angle;
                     GameObject t = Instantiate(zombie2Projectile, launchOffset.transform.position, launchOffset.transform.rotation);
                     t.tag = "Zombie2Projectile";
                    _zombieWeaponRecharging = 1;
                }
            }
            else
            {
                _zombieWeaponRecharging -= Time.deltaTime;
            }
            Movement = AI.desiredVelocity;
            animator.SetFloat(X, Movement.x);
            animator.SetFloat(Y, Movement.y);
            
        }
    }
}
