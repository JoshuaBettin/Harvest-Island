using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System;

public class CreateOrJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField createInput;

    [SerializeField]
    private Transform roomListingParent;

    [SerializeField]
    private GameObject roomListingPrefab;

    private List<RoomInfo> cachedRoomList = new List<RoomInfo>();


    public TextMeshProUGUI textfield; 

    public void Start()
    {

    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameScene");
        //  float loadPercentage = PhotonNetwork.LevelLoadingProgress;


        UpdateUI();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (cachedRoomList.Count <= 0) cachedRoomList = roomList;

        else
        {
            foreach (var room in roomList)
            {
                for (int i = 0; i < cachedRoomList.Count; i++)
                {

                    List<RoomInfo> newList = cachedRoomList;

                    if (cachedRoomList[i].Name == room.Name)
                    {
                        if (room.RemovedFromList)
                        {
                            newList.Remove(newList[i]);
                        }
                    }
                    else
                    {
                        newList.Add(room);  //room name wird falsch gesetzt? 

                    }                       // Krampf

                    cachedRoomList = newList;
                }
            }
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (Transform roomItem in roomListingParent)
        {
            Destroy(roomItem.gameObject);
        }
        
        foreach (var room in cachedRoomList)
        {
            GameObject roomItem = Instantiate(roomListingPrefab, roomListingParent);

            roomItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name;
            roomItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = room.PlayerCount + "/16";

            roomItem.transform.GetComponent<JoinRoomButton>().Name = room.Name;
        }
    }

    public void TestRooms()
    {
        GameObject roomItem = Instantiate(roomListingPrefab, roomListingParent);

    }
}