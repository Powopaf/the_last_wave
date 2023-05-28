using System;
using Photon.Pun;
using UnityEngine;

namespace Photon.C__script
{
    public class Crystal : MonoBehaviour
    {
        private int BuildRadius = 6;
        private int Health = 500;
        private int MaxHealth = 500;
        public GameObject waveSystem;

        private void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                waveSystem.SetActive(true);
            }
        }
    }
}