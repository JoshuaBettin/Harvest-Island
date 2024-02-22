using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class SpawnPlayers : MonoBehaviour
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
            GameObject Player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
            Player.GetComponentInChildren<TMP_Text>().text = PhotonNetwork.NickName;
            InstantiateKillCountUI();
        }
        else
        {
            Instantiate(playerPrefab, this.transform);
        }

    }

    private void InstantiateKillCountUI()
    {
        killCountUI.InstantiateKillCountUI(PhotonNetwork.NickName);
    }
}
