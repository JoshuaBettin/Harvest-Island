using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SandCell : GridCell
{
    public CellType type;

    private GridUpdater gridUpdater;

    public SandCell(CellType type) : base(type)
    {

    }

    private void Start()
    {
        gridUpdater = FindObjectOfType<GridUpdater>();
        this.type = CellType.Sand;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("SandCell");
        Debug.Log(eventData.hovered.Count);

        //pv.RPC("ChangeTile", RpcTarget.All, eventData.pointerCurrentRaycast.worldPosition, type);
        gridUpdater.ChangeTile(Vector3Int.FloorToInt(eventData.pointerCurrentRaycast.worldPosition), type);
    }

    [PunRPC]
    public void ChangeTile(Vector3 vector, GridCell.CellType type)
    {
     //   gridUpdater.ChangeTile(vector, type);
    }
}
