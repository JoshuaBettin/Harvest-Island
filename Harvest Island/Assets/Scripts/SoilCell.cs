using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoilCell : GridCell
{
    public CellType type;

    [SerializeField]
    private GameObject cellPrefab;

    private GridUpdater gridUpdater;
    public SoilCell(CellType type) : base(type)
    {

    }

    public void Start()
    {
        gridUpdater = FindObjectOfType<GridUpdater>();
        this.type = CellType.Soil;
    } 

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("SoilCell");
        Debug.Log(eventData.hovered.Count);
     
        gridUpdater.ChangeTile(Vector3Int.FloorToInt(eventData.pointerCurrentRaycast.worldPosition), type);
    }
}
