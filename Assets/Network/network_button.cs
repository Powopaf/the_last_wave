using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.Serialization;

public class network_button : MonoBehaviour
{
    [FormerlySerializedAs("_serverButton")] [SerializeField] private Button serverButton;
    [FormerlySerializedAs("_hostButton")] [SerializeField] private Button hostButton;
    [FormerlySerializedAs("_clientButton")] [SerializeField] private Button clientButton;

    private void Awake()
    {
        serverButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
