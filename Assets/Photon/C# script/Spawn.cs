using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Players.PlayerFolder;
using UnityEngine;

public class Spawn : MonoBehaviour
{ 
    void Start()
    {
        PhotonNetwork.Instantiate("PhotonPlayerTest", new Vector3(Random.Range(300, 310), 
                Random.Range(300, 310), -1), Quaternion.identity);
    }
}
