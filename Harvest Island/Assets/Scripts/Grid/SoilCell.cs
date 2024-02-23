using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoilCell : GridCell
{
    public CellType type;

    private GridUpdater gridUpdater;

    public SoilCell(CellType type) : base(type)
    {

    }

    private void Start()
    {
        gridUpdater = FindObjectOfType<GridUpdater>();
        this.type = CellType.Soil;
    }

    /*
    public override void OnPointerClick(PointerEventData eventData)
    { 

        //pv.RPC("ChangeTile", RpcTarget.All, eventData.pointerCurrentRaycast.worldPosition, type);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("SoilCell");
            Debug.Log(eventData.hovered.Count);

            gridUpdater.ChangeTile(Vector3Int.FloorToInt(eventData.pointerCurrentRaycast.worldPosition), type);
        }
    }
    */

    [PunRPC]
    public void ChangeTile(Vector3 vector, GridCell.CellType type)
    {
       // gridUpdater.ChangeTile(vector, type);
    }
}
