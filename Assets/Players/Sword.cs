using System;
using System.Linq;
using UnityEngine;
using Monsters;
using Photon.Pun;
using Players.PlayerFolder;

namespace Players
{
    public class Sword : MonoBehaviour
    {
        public GameObject objPlayer;
        private readonly string[] _target = { "Zombie1", "Zombie3", "Zombie4", "Zombie5" };

        private Player GetPlayer()
        {
            if (objPlayer.GetComponent<Farmer>() != null)
            {
                return objPlayer.GetComponent<Farmer>();
            }
            if (objPlayer.GetComponent<Survivor>() != null)
            {
                return objPlayer.GetComponent<Survivor>();
            }
            if (objPlayer.GetComponent<Worker>() != null)
            {
                return objPlayer.GetComponent<Worker>();
            }
            if (objPlayer.GetComponent<Assassin>() != null)
            {
                return objPlayer.GetComponent<Assassin>();
            }
            throw new Exception("Invalid player type");
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            string tagEnemy = col.gameObject.tag;
            if (_target.Contains(tagEnemy) )
            {
                var player = GetPlayer();
                int damage = player.Damage;
                switch (tagEnemy)
                {
                    case "Zombie1":
                        Zombie1 zom1 = col.gameObject.GetComponent<Zombie1>();
                        if (zom1.ZombieTakeDamage(damage))
                        {
                            PhotonNetwork.Destroy(col.gameObject);
                        }
                        col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left);
                        player.money += 10;
                    break;
                    case "Zombie3":
                        var zom3 = col.gameObject.GetComponent<Zombie3>();
                        if (zom3.ZombieTakeDamage(damage))
                        {
                            PhotonNetwork.Destroy(col.gameObject);
                        }
                        col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left);
                        player.money += 10;
                        break;
                    case "Zombie4":
                        var zom4 = col.gameObject.GetComponent<Zombie4>();
                        if (zom4.ZombieTakeDamage(damage))
                        {
                            PhotonNetwork.Destroy(col.gameObject);
                        }
                        col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left);
                        player.money += 10;
                        break;
                    case "Zombie5":
                        var zom5 = col.gameObject.GetComponent<Zombie5>();
                        if (zom5.ZombieTakeDamage(damage))
                        {
                            PhotonNetwork.Destroy(col.gameObject);
                        }
                        col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left);
                        player.money += 10;
                        break;
                }
            }
        }
    }
}