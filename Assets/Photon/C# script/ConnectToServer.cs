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
                PlayerPrefs.SetString("Username", UsernameInput.text);
                ButtonText.text = "Connecting";
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            SceneManager.LoadScene("Lobby");
            PhotonNetwork.JoinLobby();
        }
    }
}
