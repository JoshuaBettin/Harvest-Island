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
    private TMP_InputField joinExistingRoomInput;
    [SerializeField]
    private TMP_InputField nameInput;

    [SerializeField]
    private Transform roomListingParent;

    [SerializeField]
    private GameObject roomListingPrefab;


    private List<RoomInfo> cachedRoomList = new List<RoomInfo>();

    public List<RoomInfo> newList;

    bool alreadyInList = false;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinOlderRoom()
    {
        PhotonNetwork.JoinRoom(joinExistingRoomInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.NickName = nameInput.text;
        PhotonNetwork.LoadLevel("GameScene");
        //  float loadPercentage = PhotonNetwork.LevelLoadingProgress; -> level loading atm to fast to make sense

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

                    newList = cachedRoomList;
                    alreadyInList = false;

                    if (cachedRoomList[i].Name == room.Name)
                    {
                        if (room.RemovedFromList)
                        {
                            newList.Remove(newList[i]);
                        }
                     /*   if (room.PlayerCount == 0)
                        {
                            newList.Remove(newList[i]);
                        }*/
                        else if (room.PlayerCount > cachedRoomList[i].PlayerCount)
                        {
                            newList.Remove(newList[i]); // old room gets deleted and room with new playercount gets added
                                                        // kinda indirect solution
                        }
                        else
                        {
                            alreadyInList = true;
                        }
                    }
                }

                if (!alreadyInList)
                {
                    newList.Add(room);  //room name wird falsch gesetzt? 

                }                       // Krampf
                alreadyInList = false;
                cachedRoomList = newList;
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
            Debug.Log(cachedRoomList.Count);
            GameObject roomItem = Instantiate(roomListingPrefab, roomListingParent);

            roomItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name;
            roomItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = room.PlayerCount + "/16";

            roomItem.transform.GetComponent<JoinRoomButton>().Name = room.Name;
        }
    }
}