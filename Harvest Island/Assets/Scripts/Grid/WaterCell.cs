using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaterCell : GridCell, IPointerClickHandler
{
    private CellType type;

    [SerializeField]
    private GameObject cellPrefab;

    private void Start()
    {
        this.type = CellType.Water;
    }
    public WaterCell(CellType type) : base(type)
    {
        
    }

    /*
    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("WaterCell");
    }
    */
}
