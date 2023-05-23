using UnityEngine;

namespace Monsters
{
    public class Zombie2Projectile :MonoBehaviour
    {
        public float bulletspeed = 5;
        private float _lifeBullet;
        public Animator animator;
        
        void Start()
        { 
            animator = GetComponent<Animator>();
            bulletspeed = 10;
            _lifeBullet = 5;
            
        }

        void Update()
        {
            transform.position += transform.right * (Time.deltaTime * bulletspeed);
            _lifeBullet -= Time.deltaTime;
            if (_lifeBullet <= 0)
            {
                Destroy(gameObject);
            }

        }

        /*new void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.CompareTag("Player"))
            {
                Players.Survivor player = transform.GetComponent<Players.Survivor>(); //Zombie Attack
                player.ZombieDamageOnPlayer(20); // Zombie Attack 
            }

            Destroy(gameObject);
        }*/
    }
}
