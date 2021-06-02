using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;
    public SongParser parser;
    GameObject songManager;
    public GameObject songParser;
    public GameObject score;
    int scorenum = 0;
    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("TextOne").gameObject;
    }

    private void Awake()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
        
        
     // #Critical
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("blue") && PhotonNetwork.IsMasterClient && GetComponent<PhotonView>().AmOwner)
        {
            score.GetComponent<Text>().text = (scorenum + (1000 - ((collision.gameObject.GetComponent<noteScript>().timeToPlay) - collision.gameObject.GetComponent<noteScript>().getTime()))).ToString("#");
            PhotonNetwork.Destroy(collision.gameObject);
            Destroy(collision.gameObject);
            Debug.Log("it hit!!!0");
            GetComponent<AudioSource>().Play();
        } else if (collision.gameObject.name.Contains("red") && PhotonNetwork.IsMasterClient && !GetComponent<PhotonView>().AmOwner)
        {
            score.GetComponent<Text>().text = (scorenum + (1000 - ((collision.gameObject.GetComponent<noteScript>().timeToPlay) - collision.gameObject.GetComponent<noteScript>().getTime()))).ToString("#");
            PhotonNetwork.Destroy(collision.gameObject);
            Destroy(collision.gameObject);
            Debug.Log("it hit!!!0");
            GetComponent<AudioSource>().Play();
        }
    }


}
