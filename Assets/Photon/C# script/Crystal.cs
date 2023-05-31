using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace Photon.C__script
{
    public class Crystal : MonoBehaviour
    {
        private int _health = 2000;
        public GameObject waveSystem;

        void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                waveSystem.SetActive(true);
            }
        }

        void Update()
        {
            StartCoroutine(Delay5Second());
            if (_health <= 0)
            {
                GetComponent<PhotonView>().RPC("KillGame", RpcTarget.All);
            }
        }

        [PunRPC]
        public void KillGame()
        {
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.LoadLevel("Lobby");
        }

        private IEnumerator Delay5Second(int time = 5)
        {
            yield return new WaitForSeconds(time);
        }

        public void AttackCrystal(int damage)
        {
            _health -= damage;
        }
    }
}