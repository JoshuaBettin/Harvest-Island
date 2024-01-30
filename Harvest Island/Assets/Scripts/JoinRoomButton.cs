using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoomButton : MonoBehaviour
{
    public string Name; 

    public void LoadRoom()
    { 
        PhotonNetwork.JoinRoom(Name);
    }
}
