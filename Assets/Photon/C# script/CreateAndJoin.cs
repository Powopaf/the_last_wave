using System;
using Photon.C__script;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    private bool CanSelectClass;
    private int SelectCount;
    public TMP_InputField input_Create;
    public TMP_InputField input_Join;
    public GameObject LobbyPanel;
    public GameObject RoomPanel;
    public GameObject StartButton;
    public TMP_Text RoomName;
    public GameObject AssassinButton;
    public GameObject WorkerButton;
    public GameObject SurvivorButton;
    public GameObject FarmerButton;
    public GameObject SelectObject;
    public ClassSelection Selection;

    private void Awake()
    {
        DontDestroyOnLoad(SelectObject);
        CanSelectClass = true;
        SelectCount = 0;
    }

    public void CreateRoom()
    {
        if (input_Create.text.Length >= 1)
        {
            PhotonNetwork.JoinOrCreateRoom(input_Create.text, new RoomOptions() { MaxPlayers = 4, IsOpen = true } , TypedLobby.Default);
        }
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(input_Join.text);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartButton.SetActive(true);
        }
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(true);
        RoomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void JoinRoomInList(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    
    public void OnClickLoadGame()
    {
        if (PhotonNetwork.IsMasterClient && SelectCount == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel("GameScene");
        }
    }
    
    public void OnClickSelectAssassin()
    {
        if (CanSelectClass)
        {
            CanSelectClass = false;
            Selection.SelectedClass = "Assassin";
            GetComponent<PhotonView>().RPC("SelectAssassin", RpcTarget.AllBuffered);
        }
    }
        
    public void OnClickSelectWorker()
    {
        if (CanSelectClass)
        {
            CanSelectClass = false;
            Selection.SelectedClass = "Worker";
            GetComponent<PhotonView>().RPC("SelectWorker", RpcTarget.AllBuffered);
        }
    }
        
    public void OnClickSelectSurvivor()
    {
        if (CanSelectClass)
        {
            CanSelectClass = false;
            Selection.SelectedClass = "Survivor";
            GetComponent<PhotonView>().RPC("SelectSurvivor", RpcTarget.AllBuffered);
        }
    }
        
    public void OnClickSelectFarmer()
    {
        if (CanSelectClass)
        {
            CanSelectClass = false;
            Selection.SelectedClass = "Farmer";
            GetComponent<PhotonView>().RPC("SelectFarmer", RpcTarget.AllBuffered);
        }
    }
    
    [PunRPC]
    public void SelectAssassin()
    {
        AssassinButton.SetActive(false);
        SelectCount += 1;
    }
    
    [PunRPC]
    public void SelectWorker()
    {
        WorkerButton.SetActive(false);
        SelectCount += 1;
    }
    
    [PunRPC]
    public void SelectSurvivor()
    {
        SurvivorButton.SetActive(false);
        SelectCount += 1;
    }
    
    [PunRPC]
    public void SelectFarmer()
    {
        FarmerButton.SetActive(false);
        SelectCount += 1;
    }
}
