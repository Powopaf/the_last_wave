using UnityEngine;

namespace Monsters
{
    public class Zombie2projectile : Zombie2
    {
        public float bulletspeed;
        private float lifeBullet;

      
        new void Start()
        {
            animator = GetComponent<Animator>();
            bulletspeed = 10;
            lifeBullet = 5;
        }

        new void Update()
        {
            transform.position += transform.right * (Time.deltaTime * bulletspeed);
            lifeBullet -= Time.deltaTime;
            if (lifeBullet <= 0)
            {
                Destroy(gameObject);
            }

        }

        new void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.CompareTag("Player"))
            {
                Players.Survivor player = Playertarget.transform.GetComponent<Players.Survivor>(); //Zombie Attack
                player.ZombieDamageOnPlayer(20); // Zombie Attack 
            }

            Destroy(gameObject);
        }
    }
}
