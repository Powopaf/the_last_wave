using System;
using System.Linq;
using Monsters;
using Photon.Pun;
using UnityEngine;

namespace Players {
    public class Turret : MonoBehaviour
    {
        private readonly string[] _target =  { "Survivor" };

        void Start()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_target.Contains(col.transform.tag))
            {
                var p = PhotonNetwork.Instantiate("Fire", col.transform.position, Quaternion.identity);
                p.GetComponent<ParticleSystem>().Play();
                if (col.transform.CompareTag("Survivor"))
                {
                    Farmer zombie1 = col.transform.GetComponent<Farmer>(); // chenger en fonction du métier
                    if (false)
                    {
                        PhotonNetwork.Destroy(col.gameObject);
                    }
                }
            }
        }
    } 
}