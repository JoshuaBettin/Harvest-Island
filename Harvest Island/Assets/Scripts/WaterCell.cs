using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaterCell : GridCell, IPointerClickHandler
{
    private CellType type;

    [SerializeField]
    private GameObject cellPrefab;

    public WaterCell(CellType type) : base(type)
    {
        this.type = type;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("WaterCell");
    }
}
