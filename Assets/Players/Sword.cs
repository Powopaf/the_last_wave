using System;
using System.Linq;
using ExitGames.Client.Photon.StructWrapping;
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

        private void OnTriggerEnter2D(Collider2D col)
        {
            string tagEnemy = col.gameObject.tag;
            if (_target.Contains(tagEnemy))
            {
                var player = GetPlayer();
                switch (tagEnemy)
                {
                    case "Zombie1":
                        Zombie1 zom1 = col.gameObject.GetComponent<Zombie1>();
                        if (zom1.ZombieTakeDamage(player.Damage))
                        {
                            PhotonNetwork.Destroy(col.gameObject);
                        }
                        col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left);
                        player.money += 10;
                    break;
                    case "Zombie3":
                        var zom3 = col.gameObject.GetComponent<Zombie3>();
                        if (zom3.ZombieTakeDamage(player.Damage))
                        {
                            PhotonNetwork.Destroy(col.gameObject);
                        }
                        col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left);
                        player.money += 10;
                        break;
                    case "Zombie4":
                        var zom4 = col.gameObject.GetComponent<Zombie4>();
                        if (zom4.ZombieTakeDamage(player.Damage))
                        {
                            PhotonNetwork.Destroy(col.gameObject);
                        }
                        col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left);
                        player.money += 10;
                        break;
                    case "Zombie5":
                        var zom5 = col.gameObject.GetComponent<Zombie5>();
                        if (zom5.ZombieTakeDamage(player.Damage))
                        {
                            PhotonNetwork.Destroy(col.gameObject);
                        }
                        col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left);
                        player.money += 10;
                        break;
                    case "Zombie2":
                        var zom2 = col.gameObject.GetComponent<Zombie2>();
                        if (zom2.ZombieTakeDamage(player.Damage))
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