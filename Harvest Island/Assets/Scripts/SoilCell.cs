using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoilCell : GridCell
{
    private CellType type;

    [SerializeField]
    private GameObject cellPrefab; 

    public SoilCell(CellType type) : base(type)
    {
        this.type = type;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("SoilCell");  
    }
}
