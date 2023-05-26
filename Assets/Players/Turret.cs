using System.Linq;
using Monsters;
using Photon.Pun;
using UnityEngine;

namespace Players {
    public class Turret : MonoBehaviour
    {
        private string[] _target =  { "Zombie" };

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (_target.Contains(col.transform.tag))
            {
                GameObject p = PhotonNetwork.Instantiate("Fire", col.transform.position, Quaternion.identity);
                p.GetComponent<ParticleSystem>().Play();
                if (col.transform.CompareTag("Zombie1"))
                {
                    Zombie1 zombie1 = col.transform.GetComponent<Zombie1>();
                    if (zombie1.ZombieTakeDamage(5))
                    {
                        PhotonNetwork.Destroy(col.gameObject);
                    }
                }
            }
        }
    } 
}