using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("PhotonPlayer", new Vector3(Random.Range(300, 310), Random.Range(300, 310), -1),
            Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
