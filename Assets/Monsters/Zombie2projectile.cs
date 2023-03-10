using UnityEngine;

namespace Monsters
{
    public class Zombie2projectile : Zombie2
    {
        public float bulletspeed;

        public Zombie2projectile(float speed = 10)
        {
            bulletspeed = speed;
        }

        new void Start()
        {
            animator = GetComponent<Animator>();
        }

        new void Update()
        {
            transform.position += transform.right * (Time.deltaTime * bulletspeed);

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
