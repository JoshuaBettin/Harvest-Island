using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private float minX, maxX, minY, maxY;
    
    [SerializeField]
    private KillCountUI killCountUI;

    private void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

            PhotonView playerPv = player.GetPhotonView();
            int viewID = playerPv.ViewID;

            Player localPlayer = PhotonNetwork.LocalPlayer;
            string playerName = localPlayer.NickName;

            PhotonView pv = PhotonView.Get(this);
            pv.RPC("SetPlayerName", RpcTarget.AllBuffered, viewID, playerName);
            pv.RPC("InstantiateUIforKillCount", RpcTarget.AllBuffered, playerName);

        }
        else
        {
            Instantiate(playerPrefab, this.transform);
        }
    }


    [PunRPC]
    public void SetPlayerName(int viewID, string playerName)
    {
        PhotonView playerView = PhotonNetwork.GetPhotonView(viewID);
        GameObject player = playerView.gameObject;

        player.GetComponentInChildren<TMP_Text>().text = playerName;
    }

    [PunRPC]
    public void InstantiateUIforKillCount(string playerName)
    {
        killCountUI.InstantiateKillCountUI(playerName);
    }
}
