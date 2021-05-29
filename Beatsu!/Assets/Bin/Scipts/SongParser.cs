using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class SongParser : MonoBehaviourPunCallbacks
{
    public GameObject RedNote;
    public ArrayList songData;
    public float time;
    public GameObject BlueNote;
    bool start;
    // Start is called before the first frame update
    void Start()
    {
        start = true;
        songData = new ArrayList();
        songData.Add(new string[] { "10000", "0", "0", "0", "blue" });
        songData.Add(new string[] { "11000", "6", "0", "0", "blue" });
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            time += Time.deltaTime;
            if (time  * 1000 >= Int32.Parse(((string[])songData[0])[0]) - 1000)
            {
                if (((string[])songData[0])[4].Equals("blue"))
                {

                    GameObject note = PhotonNetwork.Instantiate(this.BlueNote.name, new Vector3((float)Int32.Parse(((string[])songData[0])[1]), (float)Int32.Parse(((string[])songData[0])[2]), 1), Quaternion.identity, 0);
                    note.transform.rotation = Quaternion.Euler((float)Int32.Parse(((string[])songData[0])[3]), 0, 0);
                    songData.Remove(songData[0]);
                }
                else
                {
                    GameObject note = PhotonNetwork.Instantiate(this.RedNote.name, new Vector3((float)Int32.Parse(((string[])songData[0])[1]), (float)Int32.Parse(((string[])songData[0])[2]), 1), Quaternion.identity, 0);
                    note.transform.rotation = Quaternion.Euler((float)Int32.Parse(((string[])songData[0])[3]), 0, 0);
                    songData.Remove(0);
                }

            }
        }
    }
}