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
    public GameObject canvas;
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start() {
        PhotonNetwork.NetworkingClient.EnableLobbyStatistics = true;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        
        
    }

    string playerName = "";
    string gameVersion = "1";

    public void connect()
    {


        PhotonNetwork.JoinOrCreateRoom("water", new RoomOptions { IsVisible = true, MaxPlayers = 2}, PhotonNetwork.CurrentLobby);
        startGame();
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
        text2.text = "";
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
        text1.text = "";
        foreach (Player player in PhotonNetwork.PlayerList) {
            Debug.Log(player.NickName);
            text1.text += player.NickName + "\n";
        }
       
        

    }

    public void startGame()
    {
        canvas.SetActive(false);
        
      
        LoadArena();
    }

    void LoadArena()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        }
        Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
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
        if(PlayerManager.LocalPlayerInstance == null)
        {
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
        }
    }

    public void anothermethod()
    {
        if (PlayerManager.LocalPlayerInstance == null)
        {
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
        }
    }
    

    // Update is called once per frame0
    void Update()
    {
        
    }
}
