﻿using System.Collections;
using System.Linq;
using Monsters;
using Photon.Pun;
using UnityEngine;

namespace Players {
    public class Turret : MonoBehaviour
    {
        private readonly string[] _target =  { "Zombie1" };
        public ParticleSystem particle;
        private bool CanAttack = true;
        
        private void OnTriggerStay2D(Collider2D col)
        {
            if (_target.Contains(col.transform.tag) && CanAttack)
            {
                particle.transform.position = col.transform.position;
                particle.Play();
                if (col.transform.CompareTag("Zombie1"))
                {
                    Zombie1 zombie1 = col.transform.GetComponent<Zombie1>(); // changer en fonction du zombie
                    if (zombie1.ZombieTakeDamage(5))
                    {
                        PhotonNetwork.Destroy(col.gameObject);
                    }
                }
                particle.Stop();
                StartCoroutine(DelayAttack());
            }
        }

        private IEnumerator DelayAttack()
        {
            CanAttack = false;
            yield return new WaitForSeconds(2);
            CanAttack = true;
        }
    } 
}