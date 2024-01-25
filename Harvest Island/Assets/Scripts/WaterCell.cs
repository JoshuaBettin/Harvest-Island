using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCell : GridCell
{
    private CellType type;

    [SerializeField]
    private GameObject cellPrefab;

    public WaterCell(CellType type) : base(type)
    {
        this.type = type;
    }

    public override void test()
    {
        Debug.Log("WaterCell");
    }

    public GameObject InstantiateCell()
    {
        GameObject current = GameObject.Instantiate(cellPrefab);

        return current;
    }
}
