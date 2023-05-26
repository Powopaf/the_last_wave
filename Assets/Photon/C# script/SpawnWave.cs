using System;
using Monsters;
using Photon.Pun;
using UnityEngine;
namespace Photon.C__script
{
    public class SpawnWave : MonoBehaviour
    {
        private int Wave = 0;

        private void SpawnZombie(string ZombieClass, int x, int y)
        {
            PhotonNetwork.Instantiate( ZombieClass, new Vector3(x, 
                y, -1), Quaternion.identity);
        }
        
    }
}