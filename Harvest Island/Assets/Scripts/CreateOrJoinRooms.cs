using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class CreateOrJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField createInput, joinInput;
    [SerializeField]
    private TMP_Text roomNames; 

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
       PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        roomNames.text = "Rooms: \n";
        Debug.Log("Update");

        for( int i = 0; i < roomList.Count; i++)
        {
            Debug.Log("roomName");
            roomNames.text += roomList[i].Name;
        }
    }
}
