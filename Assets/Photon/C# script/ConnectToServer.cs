using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Photon.C__script

{
    internal class ConnectToServer : MonoBehaviourPunCallbacks
    {
        public TMP_InputField UsernameInput;
        public TMP_Text ButtonText;

        public void OnClickConnect()
        {
            if (UsernameInput.text.Length >= 1)
            {
                PhotonNetwork.NickName = UsernameInput.text;
                ButtonText.text = "Connecting...";
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnJoinedLobby()
        {
            SceneManager.LoadScene("Lobby");
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }
    }
}
