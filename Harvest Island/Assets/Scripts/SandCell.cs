using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SandCell : GridCell
{
    public CellType type;

    [SerializeField]
    private GameObject cellPrefab;

    private GridUpdater gridUpdater;

    private void Start()
    {
        gridUpdater = FindObjectOfType<GridUpdater>();
        this.type = CellType.Sand;
    }
    
    public SandCell(CellType type) : base(type)
    {
        
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("SandCell");

        Debug.Log(eventData.hovered.Count);

        gridUpdater.ChangeTile(Vector3Int.FloorToInt(eventData.pointerCurrentRaycast.worldPosition), type);
    }
}
