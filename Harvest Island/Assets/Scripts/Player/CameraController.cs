using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public GameObject cameraHolder;
    [SerializeField]
    public Vector3 cameraOffset;
    [SerializeField]
    public PhotonView pv;

    private Canvas canvas;

    private void Awake()
    {
        if (PhotonNetwork.InRoom)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Canvas");
            canvas = obj.GetComponent<Canvas>();
            canvas.worldCamera = cameraHolder.GetComponent<Camera>();
        }
    }
    private void Start()
    {
        
        if(!pv.IsMine)
        {
            cameraHolder.SetActive(false);
        }
        
        if (!PhotonNetwork.IsConnected)
        {
            cameraHolder.SetActive(true);
        }
    }
    private void Update()
    {
        cameraHolder.transform.position = this.transform.position + cameraOffset;
    }
}
