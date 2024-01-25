using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SandCell : GridCell
{
    private CellType type;

    [SerializeField]
    private GameObject cellPrefab;

    public SandCell(CellType type) : base(type)
    {
        this.type = type;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("SandCell");
    }
}
