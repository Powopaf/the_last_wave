using System.Collections;
using Monsters;
using Photon.Pun;
using UnityEngine;

namespace IAPlayer
{
   
    public class IAsword: MonoBehaviour
    {
        public GameObject objPlayer;
        private readonly string[] _target = { "Zombie1", "Zombie3", "Zombie4", "Zombie5" };
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            string tagEnemy = col.gameObject.tag;
            if (((IList)_target).Contains(tagEnemy))
            {
                var player = objPlayer.GetComponent<IAplayer>(); // getcomponent IAplayer
                int damage = player.Damage;
                switch (tagEnemy)
                {
                    case "Zombie1":
                        Zombie1 zom1 = col.gameObject.GetComponent<Zombie1>();
                        if (zom1.ZombieTakeDamage(damage))
                        {
                            PhotonNetwork.Destroy(col.gameObject);
                        }

                        col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 10));
                        break;
                    case "Zombie3":
                        var zom3 = col.gameObject.GetComponent<Zombie3>();
                        if (zom3.ZombieTakeDamage(damage))
                        {
                            PhotonNetwork.Destroy(col.gameObject);
                        }

                        col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 10));
                        break;
                    case "Zombie4":
                        var zom4 = col.gameObject.GetComponent<Zombie4>();
                        if (zom4.ZombieTakeDamage(damage))
                        {
                            PhotonNetwork.Destroy(col.gameObject);
                        }

                        col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 10));
                        break;
                    case "Zombie5":
                        var zom5 = col.gameObject.GetComponent<Zombie5>();
                        if (zom5.ZombieTakeDamage(damage))
                        {
                            PhotonNetwork.Destroy(col.gameObject);
                        }

                        col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 10));
                        
                        break;
                }
            }
        }
    }
}