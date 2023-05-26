using System.Linq;
using Monsters;
using Photon.Pun;
using UnityEngine;

namespace Players {
    public class Turret : MonoBehaviour
    {
        private readonly string[] _target =  { "Survivor" };
        public ParticleSystem particle;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_target.Contains(col.transform.tag))
            {
                particle.transform.position = col.transform.position;
                particle.Play();
                if (col.transform.CompareTag("Zombie"))
                {
                    Zombie1 zombie1 = col.transform.GetComponent<Zombie1>(); // changer en fonction du zombie
                    if (zombie1.ZombieTakeDamage(5))
                    {
                        PhotonNetwork.Destroy(col.gameObject);
                    }
                }
                particle.Stop();
            }
        }
    } 
}