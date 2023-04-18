using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class IsMyCamera : MonoBehaviour
{
    public GameObject cam;
    void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            cam.SetActive(true);
        }
    }
}
