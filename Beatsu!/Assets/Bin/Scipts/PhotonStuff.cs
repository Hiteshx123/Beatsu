using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class PhotonStuff : MonoBehaviourPunCallbacks
{
    public Text text1;
    public Text text2;
    //public GameObject feild1;
    public InputField feild;
     
    // Start is called before the first frame update
    void Start() {
        PhotonNetwork.NetworkingClient.EnableLobbyStatistics = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    string playerName = "";
    string gameVersion = "1";

    public void connect()
    {


        PhotonNetwork.JoinOrCreateRoom("water", new RoomOptions { IsVisible = true, MaxPlayers = 2}, PhotonNetwork.CurrentLobby);

    }
    public static PhotonStuff water;
    private void Awake()
    {
        if (water != null && water != this)
            gameObject.SetActive(false);
        else
        {
            water = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }
    public void dissconect()
    {
        text1.text = "";
        PhotonNetwork.LeaveRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo info in roomList)
        {
            text2.text += info.Name;
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Invoke("updateList", 1f);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Invoke("updateList", 1f);
    }

    public void updateList()
    {
        foreach (Player player in PhotonNetwork.PlayerList) {
            Debug.Log(player.NickName);
            text1.text += player.NickName + "\n";
        }
       
        

    }


  

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        PhotonNetwork.JoinLobby();
    }

   public override void OnJoinedRoom()
    {
        Invoke("updateList", 1f);
        PhotonNetwork.LocalPlayer.NickName = feild.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
