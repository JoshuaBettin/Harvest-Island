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

    private void Start()
    {
        if(!pv.IsMine)
        {
            cameraHolder.SetActive(false);
        }
    }
    private void Update()
    {
        cameraHolder.transform.position = this.transform.position + cameraOffset;
    }
}
