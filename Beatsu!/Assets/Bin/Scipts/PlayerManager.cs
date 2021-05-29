using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;
    public SongParser parser;
    GameObject songManager;
    public GameObject songParser;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
        
        
 https://yt4.ggpht.com/ytc/AAUvwnjsCwL5ifqiP_rzchSjlxIYVTmMc9-cnCYnGV6eGA=s32-c-k-c0x00ffffff-no-rj       // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {

            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 1f);
        }
       
        
    }

    override public void OnPlayerEnteredRoom(Player player)
    {
        if (PhotonNetwork.PlayerList.Length == 2 && PhotonNetwork.IsMasterClient && songManager == null)
        {
            songManager = PhotonNetwork.Instantiate(songParser.name, Vector3.zero, Quaternion.identity, 0);
            parser = songManager.GetComponent<SongParser>();
            songManager.SetActive(true);
        }
    }


}
