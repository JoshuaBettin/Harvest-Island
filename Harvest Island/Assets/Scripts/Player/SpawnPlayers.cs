using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private float minX, maxX, minY, maxY;

    private void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
            PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(playerPrefab, this.transform);
        }

    }
}
