using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class noteScript : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public int timeToPlay;
    public SongParser parser;
    public Transform hitCube;
    void Start()
    {
        hitCube = transform.Find("hittime");
        hitCube.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToPlay - getTime() < -500)
        {
            PhotonNetwork.Destroy(gameObject.GetComponent<PhotonView>());
            Destroy(gameObject);
        }
        hitCube.localScale = new Vector3(1.5f * ((timeToPlay - getTime())/1000), 1.5f, 1.5f);
    }

    public float getTime()
    {
        return parser.time * 1000;
    }
}
