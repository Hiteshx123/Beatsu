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
        songData.Add(new string[] { "1000", "-6", "-5", "0", "blue" });
        songData.Add(new string[] { "4000", "3", "2", "0", "red" });
        songData.Add(new string[] { "7000", "5", "-4", "0", "red" });
        songData.Add(new string[] { "10000", "0", "0", "0", "blue" });
        songData.Add(new string[] { "13000", "6", "0", "0", "blue" });
        songData.Add(new string[] { "16000", "-6", "0", "0", "red" });
        songData.Add(new string[] { "19000", "-5", "5", "0", "blue" });
        songData.Add(new string[] { "22000", "8", "-5", "0", "red" });
        songData.Add(new string[] { "24000", "-5", "-5", "0", "red" });
        songData.Add(new string[] { "27000", "-10", "-5", "0", "blue" });
        songData.Add(new string[] { "30000", "10", "-7", "-0", "blue" });
        songData.Add(new string[] { "32000", "3", "6", "0", "red" });
        songData.Add(new string[] { "34000", "8", "1", "0", "red" });
        songData.Add(new string[] { "37000", "-8", "-4", "0", "blue" });
        songData.Add(new string[] { "39000", "8", "1", "0", "blue" });
        songData.Add(new string[] { "42000", "3", "7", "0", "red" });
        songData.Add(new string[] { "44000", "-5", "-9", "0", "red" });
        songData.Add(new string[] { "48000", "7", "-5", "0", "blue" });
        songData.Add(new string[] { "50000", "-5", "-9", "0", "red" });
        songData.Add(new string[] { "53000", "-2", "-7", "0", "red" });
        songData.Add(new string[] { "57000", "-7", "-9", "0", "red" });
        songData.Add(new string[] { "61000", "-3", "4", "0", "blue" });
        songData.Add(new string[] { "63000", "-3", "-7", "0", "blue" });
        songData.Add(new string[] { "65000", "-2", "-7", "0", "red" });
        songData.Add(new string[] { "70000", "-4", "-3", "0", "red" });
        songData.Add(new string[] { "72000", "-1", "-3", "0", "blue" });
        songData.Add(new string[] { "75000", "-1", "7", "0", "red" });
        songData.Add(new string[] { "78000", "-4", "7", "0", "red" });
        songData.Add(new string[] { "81000", "-2", "-7", "0", "blue" });
        songData.Add(new string[] { "83000", "5", "8", "0", "red" });
        songData.Add(new string[] { "85000", "8", "3", "0", "red" });

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
                    note.AddComponent<noteScript>();
                    noteScript script1 = note.GetComponent<noteScript>();
                    script1.timeToPlay = Int32.Parse(((string[])songData[0])[0]);
                    script1.parser = this;
                    songData.Remove(songData[0]);
                }
                else
                {
                    GameObject note = PhotonNetwork.Instantiate(this.RedNote.name, new Vector3((float)Int32.Parse(((string[])songData[0])[1]), (float)Int32.Parse(((string[])songData[0])[2]), 1), Quaternion.identity, 0);
                    note.transform.rotation = Quaternion.Euler((float)Int32.Parse(((string[])songData[0])[3]), 0, 0);
                    note.AddComponent<noteScript>();
                    noteScript script1 = note.GetComponent<noteScript>();
                    script1.timeToPlay = Int32.Parse(((string[])songData[0])[0]);
                    script1.parser = this;
                    songData.Remove(songData[0]);
                }

            }
        }
    }
}