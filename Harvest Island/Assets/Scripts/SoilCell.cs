using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilCell : GridCell
{
    private CellType type;

    [SerializeField]
    private GameObject cellPrefab; 

    public SoilCell(CellType type) : base(type)
    {
        this.type = type;
    }

    public override void test()
    {
        Debug.Log("SoilCell");
    }

    public GameObject InstantiateCell()
    {
        GameObject current = GameObject.Instantiate(cellPrefab);

        return current;
    }
}
